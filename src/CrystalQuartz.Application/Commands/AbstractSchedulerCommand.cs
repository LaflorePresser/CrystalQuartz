﻿namespace CrystalQuartz.Application.Commands
{
    using System;
    using System.Threading.Tasks;
    using CrystalQuartz.Core.Contracts;
    using CrystalQuartz.WebFramework.Commands;

    public abstract class AbstractSchedulerCommand<TInput, TOutput> : AbstractCommand<TInput, TOutput> 
        where TOutput : CommandResult, new()
    {
        protected AbstractSchedulerCommand(ISchedulerHostProvider schedulerHostProvider)
        {
            SchedulerHostProvider = schedulerHostProvider;
        }

        protected ISchedulerHostProvider SchedulerHostProvider { get; }

        protected SchedulerHost SchedulerHost => SchedulerHostProvider.SchedulerHost;

        public override async Task<object> Execute(TInput input)
        {
            if (SchedulerHost.Faulted)
            {
                return new TOutput
                {
                    Success = false,
                    ErrorMessage = string.Join(Environment.NewLine + Environment.NewLine, SchedulerHost.Errors)
                };
            }

            return await base.Execute(input);
        }

        protected override void HandleError(Exception exception, TInput input, TOutput output)
        {
//            var schedulerProviderException = exception as SchedulerProviderException;
//            if (schedulerProviderException != null)
//            {
//                NameValueCollection properties = schedulerProviderException.SchedulerInitialProperties;
//
//                // todo
////                output.ErrorDetails = properties
////                    .AllKeys
////                    .Select(key => new Property(key, properties.Get(key)))
////                    .ToArray();
//            }
        }
    }
}