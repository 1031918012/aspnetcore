// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.SignalR
{
    /// <summary>
    /// Context for a Hub invocation.
    /// </summary>
    public class HubInvocationContext
    {
        internal ObjectMethodExecutor ObjectMethodExecutor { get; }

        /// <summary>
        /// Instantiates a new instance of the <see cref="HubInvocationContext"/> class.
        /// </summary>
        /// <param name="context">Context for the active Hub connection and caller.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> specific to the scope of this Hub method invocation.</param>
        /// <param name="hub">The instance of the Hub.</param>
        /// <param name="hubMethod">The <see cref="MethodInfo"/> for the Hub method being invoked.</param>
        /// <param name="hubMethodArguments">The arguments provided by the client.</param>
        public HubInvocationContext(HubCallerContext context, IServiceProvider serviceProvider, Hub hub, MethodInfo hubMethod, IReadOnlyList<object> hubMethodArguments)
        {
            Hub = hub;
            ServiceProvider = serviceProvider;
            HubMethod = hubMethod;
            HubMethodArguments = hubMethodArguments;
            Context = context;

#pragma warning disable CS0618 // Type or member is obsolete
            HubMethodName = HubMethod.Name;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="HubInvocationContext"/> class.
        /// </summary>
        /// <param name="context">Context for the active Hub connection and caller.</param>
        /// <param name="hubMethodName">The name of the Hub method being invoked.</param>
        /// <param name="hubMethodArguments">The arguments provided by the client.</param>
        [Obsolete("This constructor is obsolete and will be removed in a future version. The recommended alternative is to use the other constructor.")]
        public HubInvocationContext(HubCallerContext context, string hubMethodName, object[] hubMethodArguments)
        {
            throw new NotSupportedException("This constructor no longer works. Use the other constructor.");
        }

        internal HubInvocationContext(ObjectMethodExecutor objectMethodExecutor, HubCallerContext context, IServiceProvider serviceProvider, Hub hub, object[] hubMethodArguments)
            : this(context, serviceProvider, hub, objectMethodExecutor.MethodInfo, hubMethodArguments)
        {
            ObjectMethodExecutor = objectMethodExecutor;
        }

        /// <summary>
        /// Gets the context for the active Hub connection and caller.
        /// </summary>
        public HubCallerContext Context { get; }

        /// <summary>
        /// Gets the Hub instance.
        /// </summary>
        public Hub Hub { get; }

        /// <summary>
        /// Gets the name of the Hub method being invoked.
        /// </summary>
        [Obsolete("This property is obsolete and will be removed in a future version. Use HubMethod.Name instead.")]
        public string HubMethodName { get; }

        /// <summary>
        /// Gets the arguments provided by the client.
        /// </summary>
        public IReadOnlyList<object> HubMethodArguments { get; }

        /// <summary>
        /// The <see cref="IServiceProvider"/> specific to the scope of this Hub method invocation.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// The <see cref="MethodInfo"/> for the Hub method being invoked.
        /// </summary>
        public MethodInfo HubMethod { get; }
    }
}
