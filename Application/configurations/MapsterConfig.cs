using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangement.Application.configurations
{
    using Mapster;
    using TaskMangement.Application.DTOs;
    using TaskMangement.Application.Features.Project.Commands.CreateProject;
    using TaskMangement.Application.Features.Tasks.Commands.CreateTask;
    using TaskMangement.Domain.Models;

    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<CreateTaskCommand, Domain.Models.Task>
                .NewConfig()
                .Ignore(dest => dest.Id);
            TypeAdapterConfig<CreateProjectCommand, Project>
                .NewConfig()
                .Ignore(dest => dest.Id);

            TypeAdapterConfig<Domain.Models.Task, TaskResponse>
                .NewConfig();
            TypeAdapterConfig<Project, ProjectResponse>
                .NewConfig();
            TypeAdapterConfig<ProjectResponse, Project>
                .NewConfig();

            TypeAdapterConfig<Project, ProjectTasksResponse>
                    .NewConfig()
                    .Map(dest => dest.ProjectId, src => src.Id)
                    .Map(dest => dest.ProjectName, src => src.Name);

                    TypeAdapterConfig<Task, TaskResponse>
                        .NewConfig();

        }
    }
}
