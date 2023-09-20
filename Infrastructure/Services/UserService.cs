using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Services.TokenManager;
using Shared.Utilities;
using TaskmanagerV2.Domain.Enums;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ITokenManager _tokenManager;

        public UserService(IRepositoryManager repository, ITokenManager tokenManager)
        {
            _repository = repository;
            _tokenManager = tokenManager;
        }

        public async Task<GenericResponse<Response>> CreateUser(RegisterDto registerUser)
        {
            // CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(registerUser.Name) || string.IsNullOrEmpty(registerUser.Password) || string.IsNullOrEmpty(registerUser.Email))
                throw new CustomBadRequestException("Please provide all required fields for registration.");

            // CHECK IF THE NAME IS LETTER ONLY
            if (!Util.IsInputLetterOnly(registerUser.Name))
                throw new CustomBadRequestException("Please use only letters in the name field for registration.");

            // CHECK IF EMAIL FORMAT IS CORRECT
            if (!Util.EmailIsValid(registerUser.Email))
                throw new CustomBadRequestException("Please provide a valid email address.");

            // CHECK IF PASSWORD LENGTH IS ABOVE FOUR CHARS
            if (registerUser.Password.Length < 5)
                throw new CustomBadRequestException("The password must be at least 5 characters long.");

            // CHECK IF USER ALREADY EXISTS
            var existingUser = await _repository.UserRepository.GetByEmail(registerUser.Email, false);
            if (existingUser != null)
                throw new CustomDuplicateRequestException("Sorry, a user with this email address already exists. Please use a different email.");

            // Hash the user's password for security
            var hashedPassword = Util.StringHasher(registerUser.Password);

            // Create a new User object to save in the database
            User userToSave = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow.ToString()
            };

            // Save the new user to the database
            _repository.UserRepository.CreateUser(userToSave);
            await _repository.SaveAsync();

            // Return a success response
            return new GenericResponse<Response>
            {
                IsSuccessful = true,
                ResponseCode = "201",
                ResponseMessage = "User registration successful.",
            };

        }

        public async Task<GenericResponse<Response>> DeleteUser(string userId)
        {

            //  CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(userId))
                throw new CustomBadRequestException("Please provide a User ID for user deletion.");

            // Check if the user with the given ID exists
            var userToDelete = await _repository.UserRepository.GetByUserId(userId, true);

            // CHECK IF THE USER EXISTS
            if (userToDelete == null)
                throw new CustomBadRequestException("User not found. The provided User ID does not match any existing user.");

            // Delete the user from the repository
            _repository.UserRepository.DeleteUser(userToDelete);
            await _repository.SaveAsync();

            // Return a success response
            return new GenericResponse<Response>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "User deleted successfully.",
            };

        }

        public async Task<GenericResponse<IEnumerable<UserWithIdDto>>> GetAllUsers()
        {

            // Retrieve all users from the repository
            var allUsers = await _repository.UserRepository.GetUsers();

            // Create a list to store user data
            List<UserWithIdDto> response = new List<UserWithIdDto>();

            // Automap user details and add them to the response list
            foreach (var user in allUsers)
            {
                response.Add((UserWithIdDto)user);
            }

            // Return a success response with the user data
            return new GenericResponse<IEnumerable<UserWithIdDto>>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "Users fetched successfully. Total number of users: " + allUsers.Count(),
                Data = response
            };

        }

        public async Task<GenericResponse<UserDto>> GetByUserId(string userId)
        {

            // CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(userId))
                throw new CustomBadRequestException("Please provide your User ID for user information retrieval.");

            // Check if the user with the given ID exists in the database
            var getUserFromDb = await _repository.UserRepository.GetByUserId(userId, false);

            // CHECK IF USER EXISTS
            if (getUserFromDb == null)
                throw new CustomNotFoundException("User not found. The provided User ID does not match any existing user.");

            // Fetch all assigned tasks based on the user's ID
            var getAssignedTasks = await _repository.TaskRepository.GetTasksByUserId(userId, false);

            // Create a list to store assigned task data
            List<TaskDto> assignedTasksDto = new List<TaskDto>();

            // Automap assigned task details and add them to the list
            foreach (var task in getAssignedTasks)
            {
                assignedTasksDto.Add((TaskDto)task);
            }

            // Create a response containing user information and associated tasks
            var response = new UserDto()
            {
                Name = getUserFromDb.Name,
                Email = getUserFromDb.Email,
                AssociatedTasks = assignedTasksDto.ToArray()
            };

            // Return a success response with the user and task data
            return new GenericResponse<UserDto>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "User information fetched successfully.",
                Data = response
            };


        }

        public async Task<GenericResponse<LoginResponse>> GetByEmailAndPassword(LoginDto loginRequest, bool trackChanges)
        {

            // CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
                throw new CustomBadRequestException("Please enter both your email and password for login.");

            // CHECK IF EMAIL FORMAT IS CORRECT
            if (!Util.EmailIsValid(loginRequest.Email))
                throw new CustomBadRequestException("Please provide a valid email address.");

            // Retrieve the user from the database
            var hashedPassword = Util.StringHasher(loginRequest.Password);
            var getUserFromDb = await _repository.UserRepository.GetByEmailAndPassword(loginRequest.Email, hashedPassword, trackChanges);

            // CHECK IF USER EXISTS
            if (getUserFromDb == null)
                throw new CustomNotFoundException("User not found. The provided email and password do not match any existing user.");

            // Create a response containing user information for the consumer
            var response = new LoginResponse()
            {
                UserId = getUserFromDb.UserId,
                Email = getUserFromDb.Email,
                Name = getUserFromDb.Name,
                AccessToken = getUserFromDb.AccessToken,
                RefreshToken = getUserFromDb.RefreshToken,
                CreatedAt = getUserFromDb.CreatedAt,
                TokenGenerationTime = getUserFromDb.TokenGenerationTime
            };

            // Return a success response with the user data
            return new GenericResponse<LoginResponse>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "User information fetched successfully.",
                Data = response
            };

        }

        public async Task<GenericResponse<Response>> UpdateUser(string userId, UpdateUserRequest request)
        {

            // CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(request.Name))
                throw new CustomBadRequestException("Please provide required inputs for user update.");

            // Check if the user with the given ID exists
            var userToUpdate = await _repository.UserRepository.GetByUserId(userId, true);

            // CHECK IF USER EXISTS
            if (userToUpdate == null)
                throw new CustomNotFoundException("User not found. The provided User ID does not match any existing user.");

            // Update the user's name with the provided request data
            userToUpdate.Name = request.Name;

            // Save the updated user in the repository
            _repository.UserRepository.UpdateUser(userToUpdate);
            await _repository.SaveAsync();

            // Return a success response
            return new GenericResponse<Response>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = "User updated successfully.",
            };


        }

        public async Task<GenericResponse<Response>> AssignTask(string userId, AddOrDelete operation, string taskId)
        {

            // CHECK IF REQUIRED INPUTS ARE ENTERED
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(taskId))
                throw new CustomBadRequestException("Please provide both your User ID and Task ID for task assignment or removal.");

            // Check if the user with the given ID exists in the database
            var getUserFromDb = await _repository.UserRepository.GetByUserId(userId, true);

            // CHECK IF USER EXISTS
            if (getUserFromDb == null)
                throw new CustomNotFoundException("User not found. The provided User ID does not match any existing user.");

            // Check if the task with the given ID exists in the user's tasks
            var checkIfTaskExistInUserTaskDb = await _repository.TaskRepository.GetTaskByTaskId(taskId, false);

            if (checkIfTaskExistInUserTaskDb == null)
            {
                return new GenericResponse<Response>
                {
                    IsSuccessful = false,
                    ResponseCode = "400",
                    ResponseMessage = AddOrDelete.Add == operation ? "The task you are trying to assign does not exist." : "The task you are trying to remove does not exist."
                };
            }
            else
            {
                // Transfer all current tasks of the user into a new variable for manipulation
                List<UserTask> getUserTasks = (List<UserTask>)await _repository.TaskRepository.GetTasksByUserId(userId, true);

                // This condition checks if we need to add or delete the task
                if (operation == AddOrDelete.Add)
                {
                    // Check if the task to add already exists in the user's tasks
                    if (Util.IsListContainTask(getUserTasks, checkIfTaskExistInUserTaskDb))
                    {
                        return new GenericResponse<Response>
                        {
                            IsSuccessful = false,
                            ResponseCode = "409",
                            ResponseMessage = "This task already exists in your project.",
                        };
                    }
                    else
                    {
                        // Add the new task to the user's tasks array and save it to the database
                        getUserTasks.Add(checkIfTaskExistInUserTaskDb);
                    }
                }
                else if (operation == AddOrDelete.Delete)
                {
                    // Check if the task to delete exists in the user's tasks
                    if (Util.IsListContainTask(getUserTasks, checkIfTaskExistInUserTaskDb))
                    {
                        // Remove the task from the user's tasks array and save it to the database
                        getUserTasks.RemoveAll(x => x.Id == checkIfTaskExistInUserTaskDb.Id);
                    }
                    else
                    {
                        return new GenericResponse<Response>
                        {
                            IsSuccessful = false,
                            ResponseCode = "400",
                            ResponseMessage = "This task does not exist in your project.",
                        };
                    }
                }

                getUserFromDb.UserTasks = getUserTasks;
                _repository.UserRepository.UpdateUser(getUserFromDb);
                await _repository.SaveAsync();
            }

            // Return a success response
            return new GenericResponse<Response>
            {
                IsSuccessful = true,
                ResponseCode = "200",
                ResponseMessage = AddOrDelete.Add == operation ? "User task assignment completed successfully." : "User task removal completed successfully."
            };

        }

        public async Task<GenericResponse<TokenDto>> RefreshToken(RefreshTokenDto request)
        {
            var user = await _repository.UserRepository.GetByUserId(request.UserId, false);
            //check if user is null or not
            if (user == null)
                throw new CustomNotFoundException("User is not logged in or does not exists");

            if (user.RefreshToken != request.RefreshToken)
                throw new CustomBadRequestException("Refresh token not valid");

            var loginResponse = new GenericResponse<LoginResponse>
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                Data = new LoginResponse
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                }
            };

            var tokenDto = new TokenDto
            {
                AccessToken = _tokenManager.GenerateToken(ref loginResponse),
                RefreshToken = _tokenManager.GenerateRefreshToken()
            };

            return new GenericResponse<TokenDto>
            {
                ResponseCode = "200",
                ResponseMessage = "Success",
                IsSuccessful = true,
                Data = tokenDto
            };
        }
    }
}
