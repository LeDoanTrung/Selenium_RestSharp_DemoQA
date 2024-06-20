using DemoQA.Core.API;
using DemoQA.Core.ShareData;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Models.Response;
using RestSharp;

namespace DemoQA.Service.Services
{
    public class UserService
    {
        private readonly APIClient _client;

        public UserService(APIClient client)
        {
            _client = client;
        }
        public async Task<RestResponse<CreateUserResponseDTO>> CreateUser(CreateUserRequestDTO createdUser)
        {
            return await _client.CreateRequest(APIConstant.CreateNewUserEndPoint)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(APIConstant.BearerToken)
                    .AddBody(createdUser, ContentType.Json)
                    .ExecutePostAsync<CreateUserResponseDTO>();
        }

        public async Task<RestResponse<DeleteUserResponseDTO>> DeleteUser(string userId)
        {
            string endPoint = string.Format(APIConstant.GetUserDetailEndPoint, userId);

            return await _client.CreateRequest(endPoint)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(APIConstant.BearerToken)
                    .ExecuteDeleteAsync<DeleteUserResponseDTO>();
        }

        public async Task<RestResponse<GetAllUsersResponseDTO>> GetAllUsers()
        {
            return await _client.CreateRequest(APIConstant.GetAllUsersEndPoint)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(APIConstant.BearerToken)
                    .ExecuteGetAsync<GetAllUsersResponseDTO>();
        }

        public async Task<RestResponse<GetUserDetailResponseDTO>> GetUserDetail(string userId)
        {
            string endPoint = string.Format(APIConstant.GetUserDetailEndPoint, userId);

            return await _client.CreateRequest(endPoint)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(APIConstant.BearerToken)
                    .ExecuteGetAsync<GetUserDetailResponseDTO>();
        }

        public async Task<RestResponse<UpdateUserResponseDTO>> UpdateUserDetail(string userId, UpdateUserRequestDTO updatedUser)
        {
            string endPoint = string.Format(APIConstant.GetUserDetailEndPoint, userId);

            return await _client.CreateRequest(endPoint)
                    .AddHeader("Content-Type", ContentType.Json)
                    .AddAuthorizationHeader(APIConstant.BearerToken)
                    .AddBody(updatedUser, ContentType.Json)
                    .ExecutePutAsync<UpdateUserResponseDTO>();
        }

        public void StoreDataToDeleteUser(string userId)
        {
            DataStorage.SetData("hasCreatedUser", true);
            DataStorage.SetData("userId", userId);
        }

        public void DeleteUserFromStorage() 
        {
            if ((Boolean)DataStorage.GetData("hasCreatedUser"))
            {
                this.DeleteUser(
                    (string)DataStorage.GetData("userId")
                    );
            }
        }
    }
}
