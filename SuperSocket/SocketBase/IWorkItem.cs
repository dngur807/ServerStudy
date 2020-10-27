﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Provider;

namespace SuperSocket.SocketBase
{
    /// <summary>
    /// An item can be started and stopped
    /// </summary>
    public interface IWorkItemBase : ISystemEndPoint
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the server's config.
        /// </summary>
        /// <value>
        /// The server's config.
        /// </value>
        IServerConfig Config { get; }


        /// <summary>
        /// Starts this server instance.
        /// </summary>
        /// <returns>return true if start successfull, else false</returns>
        bool Start();

  
        /// <summary>
        /// Stops this server instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets the total session count.
        /// </summary>
        int SessionCount { get; }
    }


    /// <summary>
    /// An item can be started and stopped
    /// </summary>
    public interface IWorkItem : IWorkItemBase
    {        
        /// <summary>
        /// Gets the current state of the work item.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        ServerState State { get; }
    }
}