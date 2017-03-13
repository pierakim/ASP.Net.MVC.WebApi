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
        private string Base_URL = "http://localhost:31375/api/";

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            try
            {
                var usersList = new List<UserViewModel>();

                var client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
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
                var client = new HttpClient { BaseAddress = new Uri(Base_URL) };
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
                var client = new HttpClient { BaseAddress = new Uri(Base_URL) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = client.DeleteAsync("UsersApi/"+userId).Result;
                var response = client.DeleteAsync(string.Format("UsersApi/{0}",userId)).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}