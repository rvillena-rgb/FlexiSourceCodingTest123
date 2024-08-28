using Dapper;
using FlexiSourceCodingTest.DbAccess;
using FlexiSourceCodingTest.Interfaces;
using FlexiSourceCodingTest.Models;
using FlexiSourceCodingTest.Utils;
using System.Text.Json;
using System;

namespace FlexiSourceCodingTest.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISQLDataAccess _db;
        public UserRepository(ISQLDataAccess db) => _db = db;

        public async Task<IEnumerable<GetUserProfileReturn>> GetUserProfile(string UserID)
        {
            var obj = new List<GetUserProfileReturn>();
            var param = new DynamicParameters();

            var gUtils = new GUtils();

            //Log Request
            gUtils.WriteToEventLog("Application"
                            , "FlexiSourceCodingTest"
                            , UserID
                            , System.Diagnostics.EventLogEntryType.Information);

            try
            {
                param.Add("@userID", UserID);

                var results = await _db.LoadData<GetUserProfileReturn, dynamic>("dbo.spGetUserProfile", param);
                obj.Add(new GetUserProfileReturn
                {
                   
                    StatusCode = results.First().StatusCode,
                    StatusMessage = results.First().StatusMessage,
                    Name = results.First().Name,
                    Weight = results.First().Weight,
                    Height = results.First().Height,
                    BirthDate = results.First().BirthDate,
                    Age = results.First().Age,
                    BMI = results.First().BMI,
                   
                 });

                //Log Result
                gUtils.WriteToEventLog("Application"
                , "FlexiSourceCodingTest"
                                , JsonSerializer.Serialize(obj)
                                , System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                obj.Add(new GetUserProfileReturn { StatusCode = "01", StatusMessage = e.Message });

                gUtils.WriteToEventLog("Application"
                             , "FlexiSourceCodingTest"
                             , e.ToString()
                             , System.Diagnostics.EventLogEntryType.Error);
            } 
            
            return obj.ToList();
        }

        public async Task<IEnumerable<GenericReturnStatusData<GetUserListReturn>>> GetUserList()
        {
            var obj = new List<GenericReturnStatusData<GetUserListReturn>>();
            var param = new DynamicParameters();
            
            var gUtils = new GUtils();

            try
            {
                var results = await _db.LoadData<GetUserListReturn, dynamic>("dbo.spGetUserList", param);
                obj.Add(new GenericReturnStatusData<GetUserListReturn> 
                { 
                    StatusCode = results.First().StatusCode, 
                    StatusMessage = results.First().StatusMessage, 
                    Data = results.ToList() 
                });

                //Log Result
                gUtils.WriteToEventLog("Application"
                , "FlexiSourceCodingTest"
                                , JsonSerializer.Serialize(obj)
                                , System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                obj.Add(new GenericReturnStatusData<GetUserListReturn> { StatusCode = "01", StatusMessage = e.Message });

                gUtils.WriteToEventLog("Application"
                             , "FlexiSourceCodingTest"
                             , e.ToString()
                             , System.Diagnostics.EventLogEntryType.Error);
            }

            return obj.ToList();
        }

        public async Task<IEnumerable<GetUserRunningActivityReturn>> GetUserRunningActivity(string UserID)
        {
            var obj = new List<GetUserRunningActivityReturn>();
            var param = new DynamicParameters();

            var gUtils = new GUtils();

            try
            {
                param.Add("@userID", UserID);

                var results = await _db.LoadData<GetUserRunningActivityReturn, dynamic>("dbo.spGetUserRunningActivity", param);
                obj.Add(new GetUserRunningActivityReturn
                {

                    StatusCode = results.First().StatusCode,
                    StatusMessage = results.First().StatusMessage,
                    UserID  = results.First().UserID,
                    RunningActivityID = results.First().RunningActivityID,
                    Location = results.First().Location,
                    StartDateTime = results.First().StartDateTime,
                    EndDateTime = results.First().EndDateTime,
                    Distance = results.First().Distance,
                    Duration = results.First().Duration,
                    AveragePace = results.First().AveragePace

                });

                //Log Result
                gUtils.WriteToEventLog("Application"
                , "FlexiSourceCodingTest"
                                , JsonSerializer.Serialize(obj)
                                , System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                obj.Add(new GetUserRunningActivityReturn { StatusCode = "01", StatusMessage = e.Message });

                gUtils.WriteToEventLog("Application"
                             , "FlexiSourceCodingTest"
                             , e.ToString()
                             , System.Diagnostics.EventLogEntryType.Error);
            }

            return obj.ToList();
        }

        public async Task<IEnumerable<GenericReturn>> AddUpdateUser(UserProfileParam Value)
        {
            var obj = new List<GenericReturn>();
            var param = new DynamicParameters();
            
            UserUtils userUtils = new UserUtils();
            var gUtils = new GUtils();

            //Log Request
            gUtils.WriteToEventLog("Application"
            , "FlexiSourceCodingTest"
                            , JsonSerializer.Serialize(Value)
                            , System.Diagnostics.EventLogEntryType.Information);
            try
            {
                double bmi = userUtils.GetBMI(Value.Weight, Value.Height);
                int age = userUtils.GetAge(Value.BirthDate);

                param.Add("@userID", Value.UserID);
                param.Add("@name", Value.Name);
                param.Add("@weight", Value.Weight);
                param.Add("@height", Value.Height);
                param.Add("@birthDate", Value.BirthDate);
                param.Add("@age", age);
                param.Add("@bmi", bmi);
                param.Add("@isActionAdd", Value.IsActionAdd);


                var results = await _db.LoadData<GenericReturn, dynamic>("dbo.spAddUpdateUser", param);
                obj.Add(new GenericReturn
                {
                    StatusCode = results.First().StatusCode,
                    StatusMessage = results.First().StatusMessage
                });

                //Log Result
                gUtils.WriteToEventLog("Application"
                , "FlexiSourceCodingTest"
                                , JsonSerializer.Serialize(obj)
                                , System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                obj.Add(new GenericReturn { StatusCode = "01", StatusMessage = e.Message });

                gUtils.WriteToEventLog("Application"
                             , "FlexiSourceCodingTest"
                             , e.ToString()
                             , System.Diagnostics.EventLogEntryType.Error);
            }

            return obj.ToList();
        }

        public async Task<IEnumerable<GenericReturn>> AddUserRunningActivity(UserRunningActivityParam Value)
        {
            var obj = new List<GenericReturn>();
            var param = new DynamicParameters();

            UserUtils userUtils = new UserUtils();
            var gUtils = new GUtils();

            //Log Request
            gUtils.WriteToEventLog("Application"
            , "FlexiSourceCodingTest"
                            , JsonSerializer.Serialize(Value)
                            , System.Diagnostics.EventLogEntryType.Information);
            try
            {
                TimeSpan duration = userUtils.GetDuration(Value.StartDateTime, Value.EndDateTime);
                double averagePace = userUtils.CalculateAveragePace(Value.StartDateTime, Value.EndDateTime, Value.Distance);

                param.Add("@userID", Value.UserID);
                param.Add("@location", Value.Location);
                param.Add("@startDateTime", Value.StartDateTime);
                param.Add("@endDateTime", Value.EndDateTime);
                param.Add("@distance", Value.Distance);
                param.Add("@duration", duration);
                param.Add("@averagePace", averagePace);

                var results = await _db.LoadData<GenericReturn, dynamic>("dbo.spAddUserRunningActivity", param);
                obj.Add(new GenericReturn
                {
                    StatusCode = results.First().StatusCode,
                    StatusMessage = results.First().StatusMessage
                });

                //Log Result
                gUtils.WriteToEventLog("Application"
                , "FlexiSourceCodingTest"
                                , JsonSerializer.Serialize(obj)
                                , System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                obj.Add(new GenericReturn { StatusCode = "01", StatusMessage = e.Message });

                gUtils.WriteToEventLog("Application"
                             , "FlexiSourceCodingTest"
                             , e.ToString()
                             , System.Diagnostics.EventLogEntryType.Error);
            }

            return obj.ToList();
        }

        public async Task<IEnumerable<GenericReturn>> DeleteUser(DeleteuserParam Value)
        {
            var obj = new List<GenericReturn>();
            var param = new DynamicParameters();

            var gUtils = new GUtils();

            //Log Request
            gUtils.WriteToEventLog("Application"
            , "FlexiSourceCodingTest"
                            , JsonSerializer.Serialize(Value)
                            , System.Diagnostics.EventLogEntryType.Information);

            try
            {
                param.Add("@userID", Value.UserID);

                var results = await _db.LoadData<GenericReturn, dynamic>("dbo.spDeleteUser", param);
                obj.Add(new GenericReturn
                {
                    StatusCode = results.First().StatusCode,
                    StatusMessage = results.First().StatusMessage
                });

                //Log Result
                gUtils.WriteToEventLog("Application"
                , "FlexiSourceCodingTest"
                                , JsonSerializer.Serialize(obj)
                                , System.Diagnostics.EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                obj.Add(new GenericReturn { StatusCode = "01", StatusMessage = e.Message });
            }
            return obj.ToList();
        }

        public async Task<IEnumerable<GenericReturn>> DeleteUserRunningActiviy(DeleteUserRunningActiviyParam Value)
        {
            var obj = new List<GenericReturn>();
            var param = new DynamicParameters();

            var gUtils = new GUtils();

            try
            {
                param.Add("@userID", Value.UserID);
                param.Add("@runningActivityID", Value.RunningActivityID);

                var results = await _db.LoadData<GenericReturn, dynamic>("dbo.spDeleteUserRunningActiviy", param);
                obj.Add(new GenericReturn
                {
                    StatusCode = results.First().StatusCode,
                    StatusMessage = results.First().StatusMessage
                });
            }
            catch (Exception e)
            {
                obj.Add(new GenericReturn { StatusCode = "01", StatusMessage = e.Message });

                gUtils.WriteToEventLog("Application"
                            , "FlexiSourceCodingTest"
                            , e.ToString()
                            , System.Diagnostics.EventLogEntryType.Error);
            }
            return obj.ToList();
        }
    }
}
