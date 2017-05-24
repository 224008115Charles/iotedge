﻿// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.Azure.Devices.Edge.Hub.Mqtt
{
    using System;
    using Microsoft.Azure.Devices.Edge.Hub.Core.Device;
    using Microsoft.Azure.Devices.Edge.Util;

    public class DeviceIdentity : Identity, IDeviceIdentity
    {
        readonly Lazy<string> asString;

        public DeviceIdentity(
            string iotHubHostName,
            string deviceId,
            bool isAuthenticated,
            string connectionString,
            AuthenticationScope scope,
            string policyName,
            string secret)
            : base(iotHubHostName, isAuthenticated, connectionString, scope, policyName, secret)
        {
            this.DeviceId = Preconditions.CheckNonWhiteSpace(deviceId, nameof(deviceId));
            this.asString = new Lazy<string>(
                () =>
                {
                    string policy = string.IsNullOrEmpty(this.PolicyName) ? "<none>" : this.PolicyName;
                    return $"DeviceId: {this.DeviceId} [IotHubHostName: {this.IotHubHostName}; PolicyName: {policy}; Scope: {this.Scope}]";
                });
        }

        public string DeviceId { get; }

        public override string Id => this.DeviceId;

        public override string ToString() => this.asString.Value;
    }
}