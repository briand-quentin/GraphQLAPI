using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents anyy software or device that has a command line interface");

            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor.Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommand(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("this is list of available commands for this platform");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommand(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}