﻿using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Enum;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.ViewEntity.Activity;
using ActivityCheck.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Service.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IBaseRepository<Activity> _activityRepository;

        public ActivityService(IBaseRepository<Activity> activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<BaseResponse<IEnumerable<Activity>>> GetActivities()
        {
            var response = new BaseResponse<IEnumerable<Activity>>();
            try
            {
                var activities = await _activityRepository.GetAll().ToListAsync();
                if(activities.Count() == 0)
                {
                    response.Description = "Не найдено обьектов";
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }

                response.StatusCode = StatusCode.Ok;
                response.Data = activities;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[GetActivities] : {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<BaseResponse<Activity>> GetActivity(int id)
        {
            var response = new BaseResponse<Activity>();
            try
            {
                var activity = await _activityRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
                if(activity is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = "Activity Not Found";
                    return response;
                }
                response.StatusCode = StatusCode.Ok;
                response.Data = activity;
                return response;
            }catch(Exception ex)
            {
                response.Description = $"[GetActivity] : {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<BaseResponse<Activity>> CreateActivity(ActivityViewModel activityView)
        {
            var response = new BaseResponse<Activity>();
            try
            {
                Activity activity = new Activity()
                {
                    Name = activityView.Name,
                    Description = activityView.Description,
                    Created = DateTime.Now,
                    DurationInSec = activityView.DurationInSec,
                };

                await _activityRepository.Create(activity);

                response.StatusCode = StatusCode.Ok;
                response.Data = activity;
                return response;

            }catch (Exception ex)
            {
                response.Description = $"[CreateActivity] : {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<BaseResponse<bool>> DeleteActivity(int id)
        {
            var response = new BaseResponse<bool>() { Data = true };
            try
            {
                var activity = await _activityRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
                if(activity is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = "Unable To Found Activity";
                    response.Data = false;
                    return response;
                }

                await _activityRepository.Delete(activity);
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = $"[DeleteActivity]: {ex.Message}";
                response.Data = false;
                return response;
            }
        }

        public async Task<BaseResponse<Activity>> EditActivity(int id, ActivityViewModel model)
        {
            var response = new BaseResponse<Activity>();
            try
            {
                var activity = await _activityRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == id);
                if(activity is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = "Activity Not Found";
                    return response;
                }

                activity.From(model);

                await _activityRepository.Update(activity);
                response.StatusCode = StatusCode.Ok;
                response.Data = activity;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = $"[Edit]: {ex.Message}";
                return response;
            }
        }
        public async Task<BaseResponse<IEnumerable<Activity>>>  GetActivitiesByDate(DateTime date, long userId)
        {
            var response = new BaseResponse<IEnumerable<Activity>>();
            try
            {

                var activities = await _activityRepository.GetAll().Where(x=>x.Created.Date == date.Date && x.UserId == userId).ToListAsync();

                if (activities.Count() == 0)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = "ActivitiesByDate Not Found";
                    return response;
                }
                response.Data = activities;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = $"[GetActivitiesByDate]: {ex.Message}";
                return response;
            }


        }

        public async Task<BaseResponse<IEnumerable<Activity>>> GetActivities(long userId)
        {
            var response = new BaseResponse<IEnumerable<Activity>>();

            try
            {
                var activitiesResp = _activityRepository.GetAll().Where(x=>x.UserId == userId);

                if(activitiesResp is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Description = "Нет активностей";
                    return response;
                }

                response.Data = activitiesResp;
                response.StatusCode = StatusCode.Ok;
                return response;

            }
            catch(Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Description = $"[GetActivitiesWithUserId]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<Activity>> CreateActivity(ActivityViewModel activityView, long userId)
        {
            var response = new BaseResponse<Activity>();
            try
            {
                Activity activity = new Activity()
                {
                    Name = activityView.Name,
                    Description = activityView.Description,
                    Created = DateTime.Now,
                    DurationInSec = activityView.DurationInSec,
                    UserId = userId
                };

                await _activityRepository.Create(activity);

                response.StatusCode = StatusCode.Ok;
                response.Data = activity;
                return response;

            }
            catch (Exception ex)
            {
                response.Description = $"[CreateActivity] : {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }
    }
}
