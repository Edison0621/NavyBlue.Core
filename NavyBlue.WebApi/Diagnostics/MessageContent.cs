// ***********************************************************************
// Project          : MoeLib
// File             : MessageContent.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  2:50 PM
// ***********************************************************************
// <copyright file="MessageContent.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace MoeLib.Diagnostics
{
    /// <summary>
    ///     MessageContent.
    /// </summary>
    public class MessageContent : TraceEntry, IEquatable<MessageContent>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageContent" /> class.
        /// </summary>
        public MessageContent()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageContent" /> class.
        /// </summary>
        /// <param name="traceEntry">The trace entry.</param>
        public MessageContent(TraceEntry traceEntry)
        {
            if (traceEntry != null)
            {
                this.ClientId = traceEntry.ClientId;
                this.DeviceId = traceEntry.DeviceId;
                this.RequestId = traceEntry.RequestId;
                this.SessionId = traceEntry.SessionId;
                this.SourceIP = traceEntry.SourceIP;
                this.SourceUserAgent = traceEntry.SourceUserAgent;
                this.UserId = traceEntry.UserId;
            }
        }

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
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public Dictionary<string, object> Payload { get; set; }

        /// <summary>
        ///     Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }

        #region IEquatable<MessageContent> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(MessageContent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.ErrorCode == other.ErrorCode && string.Equals(this.ErrorCodeMsg, other.ErrorCodeMsg) && string.Equals(this.Message, other.Message) && Equals(this.Payload, other.Payload) && string.Equals(this.Tag, other.Tag);
        }

        #endregion IEquatable<MessageContent> Members

        /// <summary>
        ///     Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MessageContent left, MessageContent right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MessageContent left, MessageContent right)
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
            return this.Equals((MessageContent)obj);
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
                int hashCode = this.ErrorCode.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.ErrorCodeMsg != null ? this.ErrorCodeMsg.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Message != null ? this.Message.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Payload != null ? this.Payload.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tag != null ? this.Tag.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}