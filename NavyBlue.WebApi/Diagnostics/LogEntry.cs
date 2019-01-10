// ***********************************************************************
// Project          : MoeLib
// File             : LogEntry.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-26  11:55 PM
// ***********************************************************************
// <copyright file="LogEntry.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace MoeLib.Diagnostics
{
    /// <summary>
    ///     LogEntry.
    /// </summary>
    public class LogEntry : IEquatable<LogEntry>
    {
        /// <summary>
        ///     Gets or sets the deployment identifier.
        /// </summary>
        /// <value>The deployment identifier.</value>
        public string DeploymentId { get; set; }

        /// <summary>
        ///     Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public ulong ErrorCode { get; set; }

        /// <summary>
        ///     Gets or sets the error code MSG.
        /// </summary>
        /// <value>The error code MSG.</value>
        public string ErrorCodeMsg { get; set; }

        /// <summary>
        ///     Gets or sets the event identifier.
        /// </summary>
        /// <value>The event identifier.</value>
        public string EventId { get; set; }

        /// <summary>
        ///     Gets or sets the function.
        /// </summary>
        /// <value>The function.</value>
        public string Function { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public MessageContent Message { get; set; }

        /// <summary>
        ///     Gets or sets the precise timestamp.
        /// </summary>
        /// <value>The precise timestamp.</value>
        public DateTime PreciseTimeStamp { get; set; }

        /// <summary>
        ///     Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        /// <summary>
        ///     Gets or sets the role instance.
        /// </summary>
        /// <value>The role instance.</value>
        public string RoleInstance { get; set; }

        #region IEquatable<LogEntry> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(LogEntry other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.DeploymentId, other.DeploymentId) && this.ErrorCode == other.ErrorCode && string.Equals(this.ErrorCodeMsg, other.ErrorCodeMsg) && string.Equals(this.EventId, other.EventId) && string.Equals(this.Function, other.Function) && this.Level == other.Level && Equals(this.Message, other.Message) && this.PreciseTimeStamp.Equals(other.PreciseTimeStamp) && string.Equals(this.Role, other.Role) && string.Equals(this.RoleInstance, other.RoleInstance);
        }

        #endregion IEquatable<LogEntry> Members

        /// <summary>
        ///     Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(LogEntry left, LogEntry right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(LogEntry left, LogEntry right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        ///     true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((LogEntry)obj);
        }

        /// <summary>
        ///     Serves as the default hash function.
        /// </summary>
        /// <returns>
        ///     A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.DeploymentId != null ? this.DeploymentId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.ErrorCode.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.ErrorCodeMsg != null ? this.ErrorCodeMsg.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EventId != null ? this.EventId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Function != null ? this.Function.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Level;
                hashCode = (hashCode * 397) ^ (this.Message != null ? this.Message.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.PreciseTimeStamp.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Role != null ? this.Role.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RoleInstance != null ? this.RoleInstance.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}