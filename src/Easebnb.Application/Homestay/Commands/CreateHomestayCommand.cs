using Easebnb.Domain.Common.Services;
using Easebnb.Domain.Homestay;
using Easebnb.Domain.Homestay.Services;
using Easebnb.Shared;

namespace Easebnb.Application.Homestay.Commands;

public sealed class CreateHomestayCommand : ICommand<ErrorOr<bool>>
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public string Address { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

public sealed class CreateHomestayCommandHandler : ICommandHandler<CreateHomestayCommand, ErrorOr<bool>>
{
    private readonly ISystemIdGenService _systemIdGenService;
    private readonly IHomestayService _homestayService;

    public CreateHomestayCommandHandler(ISystemIdGenService systemIdGenService, IHomestayService homestayService)
    {
        _systemIdGenService = systemIdGenService;
        _homestayService = homestayService;
    }

    public async Task<ErrorOr<bool>> Handle(CreateHomestayCommand request, CancellationToken cancellationToken)
    {
        var homestay = HomestayEntity.Create(_systemIdGenService.GenerateLongId(), request.Name, request.Description);
        HomestayEntity.SetAddress(homestay, request.Address, request.Longitude, request.Latitude);
        await _homestayService.CreateHomstayAsync(homestay);
        return true;
    }
}