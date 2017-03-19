using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using TrainingASP.Models;

namespace TrainingASP.Client
{
    public class UsersClient
    {
        private const string BaseUrl = "http://localhost:31375/api/";

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            try
            {
                var usersList = new List<UserViewModel>();

                var client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("UsersApi").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    dynamic results = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    foreach (var item in results)
                    {
                        var temp = new UserViewModel
                        {
                            UserId = item.UserId,
                            UserName = item.UserName.ToString()
                        };
                        usersList.Add(temp);
                    }
                }
                return usersList;
            }
            catch
            {
                return null;
            }
        }

        public bool Create(UserViewModel user)
        {
            try
            {
                var client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("UsersApi", user).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int userId)
        {
            try
            {
                var client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.DeleteAsync($"UsersApi/{userId}").Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserViewModel Get(int userId)
        {
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync($"UsersApi/{userId}").Result;

                var uvm = new UserViewModel();
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    dynamic results = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    uvm.UserId = results.UserId;
                    uvm.UserName = results.UserName.ToString();
                }
                return uvm;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Edit(UserViewModel user)
        {
            try
            {
                var client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = client.PutAsJsonAsync(string.Format("UsersApi/{0}",user.UserId), user).Result;
                var response = client.PutAsJsonAsync("UsersApi/" + user.UserId, user).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}