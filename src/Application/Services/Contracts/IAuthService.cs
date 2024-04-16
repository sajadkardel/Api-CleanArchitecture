using Application.Dtos.Auth;

namespace Application.Services.Contracts;

public interface IAuthService
{
    public Task<AccessTokenResponseDto> SignUpAsync(SignUpRequestDto request, CancellationToken cancellationToken = default);
    public Task<AccessTokenResponseDto> SignInAsync(SignInRequestDto request, CancellationToken cancellationToken = default);
    public Task SignOutAsync(SignOutRequestDto request, CancellationToken cancellationToken = default);
}
