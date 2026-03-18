using Application.DTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces.IUserInterface;
using Domain.Entities.UserEntities;
using MediatR;

namespace Application.Interactors.UserInteractors
{
    #region GetAllTestUser
    public class GetAllQuery : IRequest<PaginatedResponse<UserResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new PaginationRequest();

    }

    public class GetAllUserInteractor : IRequestHandler<GetAllQuery, PaginatedResponse<UserResponse>>
    {
        private readonly IUserRepository _users;

        public GetAllUserInteractor(IUserRepository users)
        {
            _users = users;
        }

        public async Task<PaginatedResponse<UserResponse>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _users.GetAllWithFilterAsync(request.Pagination);
            var response = new PaginatedResponse<UserResponse>
            {
                ItemsCount = result.ItemsCount,
                Items = result.Items.Select(MapToTaskResponse).ToList()
            };
            return response;
        }

        private static UserResponse MapToTaskResponse(User u) => new()
        {
            Email = u.Email,
            Username = u.UserName
        };
    }
    #endregion

}
