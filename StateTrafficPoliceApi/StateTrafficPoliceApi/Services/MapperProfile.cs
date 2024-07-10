using AutoMapper;
using StateTrafficPoliceApi.IdxDtos;
using StateTrafficPoliceApi.IdxDtos.AutoHistory;
using StateTrafficPoliceApi.StfDtos.Auto;
using StateTrafficPoliceApi.StfDtos.Driver;

namespace StateTrafficPoliceApi.Services;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<StfDriverResponseDTO, DrivingLicenseDTO>()
            .ForMember(x => x.ResultMessage, opt => opt.MapFrom(xx => xx.Message))
            .ForMember(x => x.ResultCode, opt => opt.MapFrom(xx => xx.Code))
            .ForMember(x => x.PersonBirthDate, opt => opt.MapFrom(xx => xx.Doc.Bdate.ToString("dd.MM.yyyy")))
            .ForMember(x => x.DrivingLicenseNumber, opt => opt.MapFrom(xx => xx.Doc.Num))
            .ForMember(x => x.DrivingLicenseIssueDate, opt => opt.MapFrom(xx => xx.RequestTime.Split(" ", StringSplitOptions.None)[0]))
            .ForMember(x => x.DrivingLicenseExpiryDate, opt => opt.MapFrom(xx => xx.Doc.Srok.ToString("dd.MM.yyyy")))
            .ForMember(x => x.DrivingLicenseCategory, opt => opt.MapFrom(xx => xx.Doc.Cat))
            .ForMember(x => x.DecisionList, opt => opt.MapFrom(xx => xx.Decis));

        CreateMap<StfAutoResponseDTO, AutoHistoryDTO>()
            .ForMember(x => x.OwnershipPeriods, opt => opt.MapFrom(xx => xx.RequestResult.Periods))
            .ForMember(x => x.Vehicle, opt => opt.MapFrom(xx => xx));

        CreateMap<StfAutoResponseDTO, Vehicle>()
            .ForMember(x => x.BodyNumber, opt => opt.MapFrom(xx => xx.RequestResult.VehicleBodyNumber))
            .ForMember(x => x.Vin, opt => opt.MapFrom(xx => xx.Vin))
            .ForMember(x => x.Model, opt => opt.MapFrom(xx => xx.RequestResult.VehicleBrandmodel))
            .ForMember(x => x.Type, opt => opt.MapFrom(xx => xx.RequestResult.VehicleBodyType))
            .ForMember(x => x.Year, opt => opt.MapFrom(xx => xx.RequestResult.VehicleReleaseYear));

        CreateMap<StfAutoPeriod, OwnershipPeriod>()
            .ForMember(x => x.SimplePersonType, opt => opt.MapFrom(xx => xx.OwnerType))
            .ForMember(x => x.From, opt => opt.MapFrom(xx => xx.StartDate))
            .ForMember(x => x.To, opt => opt.MapFrom(xx => xx.EndDate));
    }
}
