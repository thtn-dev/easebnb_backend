
using Easebnb.Domain.Homestay;
using Easebnb.Domain.Homestay.Services;
using Easebnb.Shared;

namespace Easebnb.Application.Homestay.Queries;
 public class GetHomestayByIdQuery : IQuery<ErrorOr<HomestayEntity>>
{
    public long Id { get; set; }
}

public class GetHomestayByIdQueryHandle : IQueryHandler<GetHomestayByIdQuery, ErrorOr<HomestayEntity>>
{
    private readonly IHomestayService _homestayService;

    public GetHomestayByIdQueryHandle(IHomestayService homestayService)
    {
        _homestayService = homestayService;
    }

    public async Task<ErrorOr<HomestayEntity>> Handle(GetHomestayByIdQuery request, CancellationToken cancellationToken)
    {
        var homestay = await _homestayService.GetHomestayByIdAsync(request.Id);
        if(homestay == null)
        {
            return Error.NotFound();
        }
        return homestay;
    }
}
