using GymPlanner.Application.Models.Plan;
using GymPlanner.Domain.Entities.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GymPlanner.Application.Mappings
{
    public class PlanMappingProfile : Profile
    {
        public PlanMappingProfile()
        {
            CreateMap<PlanEditDto, Plan>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PlanId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.MenuDescription, opt => opt.MapFrom(src => src.MenuDescription))
                .ForMember(dest => dest.TagsDb, opt => opt.MapFrom(src => src.TagsString));

            CreateMap<Plan, PlanEditDto>()
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MenuDescription, opt => opt.MapFrom(src => src.MenuDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ExerciseFrequencies, opt => opt.MapFrom(src => src.planExersiseFrequencies))
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.planExersiseFrequencies.Select(pf => pf.Exercise).Distinct()))
                .ForMember(dest => dest.Frequencies, opt => opt.MapFrom(src => src.planExersiseFrequencies.Select(pf => pf.Frequency).Distinct()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.TagsString, opt => opt.MapFrom(src => src.TagsDb))
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore()); 

        CreateMap<Plan, PlanDetailsDto>()
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MenuDescription, opt => opt.MapFrom(src => src.MenuDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ExerciseFrequencies, opt => opt.MapFrom(src => src.planExersiseFrequencies))
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.planExersiseFrequencies.Select(pf => pf.Exercise).Distinct()))
                .ForMember(dest => dest.Frequencies, opt => opt.MapFrom(src => src.planExersiseFrequencies.Select(pf => pf.Frequency).Distinct()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.TagsString, opt => opt.MapFrom(src => src.TagsDb))
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
                .ForMember(dest => dest.IsSubscribed, opt => opt.Ignore());

            CreateMap<PlanExerciseFrequency, ExerciseFrequencyDto>()
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.ExerciseId))
                .ForMember(dest => dest.FrequencyId, opt => opt.MapFrom(src => src.FrequencyId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Plan, GetPlansOnIndexDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.MenuDescription, opt => opt.MapFrom(src => src.MenuDescription))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagsDb));

            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<Frequency, FrequencyDto>().ReverseMap();
        }
    }
}
