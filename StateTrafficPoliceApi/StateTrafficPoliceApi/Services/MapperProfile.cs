using AutoMapper;
using StateTrafficPoliceApi.IdxDtos.Auto.DiagnosticCard;
using StateTrafficPoliceApi.IdxDtos.Auto.DTP;
using StateTrafficPoliceApi.IdxDtos.Auto.Fines;
using StateTrafficPoliceApi.IdxDtos.Auto.Hostory;
using StateTrafficPoliceApi.IdxDtos.Auto.Restrict;
using StateTrafficPoliceApi.IdxDtos.Auto.Wanted;
using StateTrafficPoliceApi.IdxDtos.Driver;
using StateTrafficPoliceApi.StfDtos.Auto.DiagnosticCard;
using StateTrafficPoliceApi.StfDtos.Auto.DTP;
using StateTrafficPoliceApi.StfDtos.Auto.Fines;
using StateTrafficPoliceApi.StfDtos.Auto.History;
using StateTrafficPoliceApi.StfDtos.Auto.Restrict;
using StateTrafficPoliceApi.StfDtos.Auto.Wanted;
using StateTrafficPoliceApi.StfDtos.Driver;
using System.Globalization;

namespace StateTrafficPoliceApi.Services;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        #region Driver

        CreateMap<StfDriverResponseDTO, IdxDrivingLicenseDTO>()
            .ForMember(x => x.PersonBirthDate, opt => opt.MapFrom(xx => xx.Doc.Bdate.ToString("dd.MM.yyyy")))
            .ForMember(x => x.DrivingLicenseNumber, opt => opt.MapFrom(xx => xx.Doc.Num))
            .ForMember(x => x.DrivingLicenseIssueDate, opt => opt.MapFrom(xx => xx.RequestTime.Split(" ", StringSplitOptions.None)[0]))
            .ForMember(x => x.DrivingLicenseExpiryDate, opt => opt.MapFrom(xx => xx.Doc.Srok.ToString("dd.MM.yyyy")))
            .ForMember(x => x.DrivingLicenseCategory, opt => opt.MapFrom(xx => xx.Doc.Cat))
            .ForMember(x => x.DecisionList, opt => opt.MapFrom(xx => xx.Decis))
            .ForMember(x => x.Wanted, opt => opt.MapFrom(xx => xx.Wanted != null ? $"Документ не действителен и разыскивается с {xx.Wanted.DateWanted:d}" : ""))
            .ForMember(x => x.Description, opt => opt.MapFrom(xx => xx.Doc.StKart != "T" ? "Недействителен" : "Действует"));

        CreateMap<StfDecisionDTO, IdxDecisionDTO>()
            .ForMember(x => x.Date, opt => opt.MapFrom(xx => xx.Date.ToString("dd.MM.yyyy")))
            .ForMember(x => x.Period, opt => opt.MapFrom(xx => xx.Srok.ToString()))
            .ForMember(x => x.BirthPlace, opt => opt.MapFrom(xx => xx.BPlace))
            .ForMember(x => x.RegName, opt => opt.MapFrom(xx => xx.RegionName));

        #endregion

        #region Auto

        #region History

        CreateMap<StfAutoHistoryResponseDTO, IdxAutoHistoryDTO>()
            .ForMember(x => x.OwnershipPeriods, opt => opt.MapFrom(xx => xx.RequestResult.Periods))
            .ForMember(x => x.Vehicle, opt => opt.MapFrom(xx => xx));

        CreateMap<StfAutoHistoryResponseDTO, IdxVehicle>()
            .ForMember(x => x.BodyNumber, opt => opt.MapFrom(xx => xx.RequestResult.VehicleBodyNumber))
            .ForMember(x => x.Vin, opt => opt.MapFrom(xx => xx.Vin))
            .ForMember(x => x.Model, opt => opt.MapFrom(xx => xx.RequestResult.VehicleBrandmodel))
            .ForMember(x => x.Type, opt => opt.MapFrom(xx => xx.RequestResult.VehicleBodyType))
            .ForMember(x => x.Year, opt => opt.MapFrom(xx => xx.RequestResult.VehicleReleaseYear));

        CreateMap<StfAutoPeriod, IdxOwnershipPeriod>()
            .ForMember(x => x.SimplePersonType, opt => opt.MapFrom(xx => xx.OwnerType))
            .ForMember(x => x.From, opt => opt.MapFrom(xx => xx.StartDate))
            .ForMember(x => x.To, opt => opt.MapFrom(xx => xx.EndDate));

        #endregion

        #region DTP

        CreateMap<StfAutoDTPResponseDTO, IdxAutoDtpDTO>()
            .ForMember(x => x.DtpList, opt => opt.MapFrom(xx => xx.RequestResult.Accidents))
            .ForMember(x => x.Status, opt => opt.Ignore());

        CreateMap<StfAccidentDTO, IdxAccidentDTO>()
            .ForMember(x => x.DtpRegion, opt => opt.MapFrom(xx => xx.AccidentPlace))
            .ForMember(x => x.DtpNumber, opt => opt.MapFrom(xx => xx.AccidentNumber))
            .ForMember(x => x.DtpType, opt => opt.MapFrom(xx => xx.AccidentType))
            .ForMember(x => x.DtpMoment, opt => opt.MapFrom(xx => xx.AccidentDateTime))
            .ForMember(x => x.Model, opt => opt.MapFrom(xx => $"{xx.VehicleMark} {xx.VehicleModel}"))
            .ForMember(x => x.Year, opt => opt.MapFrom(xx => xx.VehicleYear))
            .ForMember(x => x.DamageState, opt => opt.MapFrom(xx => xx.VehicleDamageState));

        #endregion

        #region Diagnostic card

        CreateMap<ConvertedAutoDCResponseDTO, IdxAutoDcListDTO>();

        CreateMap<StfAutoShortDcDTO, IdxAutoDcDTO>()
            .ForMember(x => x.Status, opt => opt.MapFrom(xx => xx.DcExpirationDate > DateTime.Now ? "active" : "inactive"))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(xx => xx.DcDate.ToString("dd.MM.yyyy")))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(xx => xx.DcExpirationDate.ToString("dd.MM.yyyy")))
            .ForMember(x => x.Number, opt => opt.MapFrom(xx => xx.DcNumber));

        #endregion

        #region Fines

        CreateMap<StfAutoFinesResponseDTO, IdxAutoFinesListDTO>()
            .ForMember(x => x.FinesList, opt => opt.MapFrom(xx => xx.Data));

        CreateMap<StfAutoFinesDataDTO, IdxAutoFineDTO>()
            .ForMember(x => x.DateDecis, opt => opt.MapFrom(xx => $"{DateTime.ParseExact(xx.DateDecis.Split(" ", StringSplitOptions.None)[0], "yyyy-MM-dd", null):dd.MM.yyyy} {xx.DateDecis.Split(" ", StringSplitOptions.None)[1]}"))
            .ForMember(x => x.DateDiscount, opt => opt.MapFrom(xx => $"{DateTime.ParseExact(xx.DateDiscount.Split(" ", StringSplitOptions.None)[0], "yyyy-MM-dd", null):dd.MM.yyyy} {xx.DateDiscount.Split(" ", StringSplitOptions.None)[1]}"))
            .ForMember(x => x.DatePost, opt => opt.MapFrom(xx => xx.DatePost.ToString("dd.MM.yyyy")))
            .ForMember(x => x.DivisionName, opt => opt.MapFrom(xx => xx.Divisions[xx.Division.ToString()]["name"].ToString()));

        #endregion

        #region Wanted

        CreateMap<StfAutoWantedResponseDTO, IdxAutoWantedListDTO>()
            .ForMember(x => x.WantedList, opt => opt.MapFrom(xx => xx.RequestResult.Records))
            .ForMember(x => x.Status, opt => opt.Ignore());

        CreateMap<StfAutoWantedDTO, IdxAutoWantedDTO>()
            .ForMember(x => x.Year, opt => opt.MapFrom(xx => xx.VehicleYear))
            .ForMember(x => x.Region, opt => opt.MapFrom(xx => xx.RegionIniciator))
            .ForMember(x => x.WantedSince, opt => opt.MapFrom(xx => xx.PermanentAccountingDate));

        #endregion

        #region Restrict

        CreateMap<StfAutoRestrictResponseDTO, IdxAutoRestrictListDTO>()
            .ForMember(x => x.RestrictList, opt => opt.MapFrom(xx => xx.RequestResult.Records))
            .ForMember(x => x.Status, opt => opt.Ignore());

        CreateMap<StfAutoRestrictDTO, IdxAutoRestrictDTO>()
            .ForMember(x => x.Model, opt => opt.MapFrom(xx => xx.Tsmodel))
            .ForMember(x => x.Year, opt => opt.MapFrom(xx => xx.Tsyear))
            .ForMember(x => x.RestrictCause, opt => opt.MapFrom(xx => xx.OsnOgr))
            .ForMember(x => x.RestrictPhone, opt => opt.MapFrom(xx => xx.Phone))
            .ForMember(x => x.RestrictDate, opt => opt.MapFrom(xx => xx.Dateogr))
            .ForMember(x => x.RestrictRegion, opt => opt.MapFrom(xx => xx.Regname));

        #endregion

        #endregion
    }
}
