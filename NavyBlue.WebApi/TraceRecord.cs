// *****************************************************************************************************************
// Project          : NavyBlue
// File             : TraceRecord.cs
// Created          : 2019-01-10  10:13
// 
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  10:13
// *****************************************************************************************************************
// <copyright file="TraceRecord.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2018 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace NavyBlue.WebApi
{
    /// <summary>Represents a trace record.</summary>
    [DebuggerDisplay("Category: {Category}, Operation: {Operation}, Level: {Level}, Kind: {Kind}")]
    public class TraceRecord
    {
        private Lazy<Dictionary<object, object>> _properties = new Lazy<Dictionary<object, object>>((Func<Dictionary<object, object>>)(() => new Dictionary<object, object>()));
        private TraceKind _traceKind;
        private TraceLevel _traceLevel;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Http.Tracing.TraceRecord" /> class.</summary>
        /// <param name="request">The message request.</param>
        /// <param name="category">The trace category.</param>
        /// <param name="level">The trace level.</param>
        public TraceRecord(HttpRequestMessage request, string category, TraceLevel level)
        {
            this.Timestamp = DateTime.UtcNow;
            this.Request = request;
            this.RequestId = request != null ? request.GetCorrelationId() : Guid.Empty;
            this.Category = category;
            this.Level = level;
        }

        /// <summary>Gets or sets the tracing category.</summary>
        /// <returns>The tracing category.</returns>
        public string Category { get; set; }

        /// <summary>Gets or sets the exception.</summary>
        /// <returns>The exception.</returns>
        public Exception Exception { get; set; }

        /// <summary>Gets or sets the kind of trace.</summary>
        /// <returns>The kind of trace.</returns>
        public TraceKind Kind
        {
            get
            {
                return this._traceKind;
            }
            set
            {
                TraceKindHelper.Validate(value, nameof(value));
                this._traceKind = value;
            }
        }

        /// <summary>Gets or sets the tracing level.</summary>
        /// <returns>The tracing level.</returns>
        public TraceLevel Level
        {
            get
            {
                return this._traceLevel;
            }
            set
            {
                TraceLevelHelper.Validate(value, nameof(value));
                this._traceLevel = value;
            }
        }

        /// <summary>Gets or sets the message.</summary>
        /// <returns>The message.</returns>
        public string Message { get; set; }

        /// <summary>Gets or sets the logical operation name being performed.</summary>
        /// <returns>The logical operation name being performed.</returns>
        public string Operation { get; set; }

        /// <summary>Gets or sets the logical name of the object performing the operation.</summary>
        /// <returns>The logical name of the object performing the operation.</returns>
        public string Operator { get; set; }

        /// <summary>Gets the optional user-defined properties.</summary>
        /// <returns>The optional user-defined properties.</returns>
        public Dictionary<object, object> Properties
        {
            get
            {
                return this._properties.Value;
            }
        }

        /// <summary>Gets the <see cref="T:System.Net.Http.HttpRequestMessage" /> from the record.</summary>
        /// <returns>The <see cref="T:System.Net.Http.HttpRequestMessage" /> from the record.</returns>
        public HttpRequestMessage Request { get; private set; }

        /// <summary>Gets the correlation ID from the <see cref="P:System.Web.Http.Tracing.TraceRecord.Request" />.</summary>
        /// <returns>The correlation ID from the <see cref="P:System.Web.Http.Tracing.TraceRecord.Request" />.</returns>
        public Guid RequestId { get; private set; }

        /// <summary>Gets or sets the <see cref="T:System.Net.HttpStatusCode" /> associated with the <see cref="T:System.Net.Http.HttpResponseMessage" />.</summary>
        /// <returns>The <see cref="T:System.Net.HttpStatusCode" /> associated with the <see cref="T:System.Net.Http.HttpResponseMessage" />.</returns>
        public HttpStatusCode Status { get; set; }

        /// <summary>Gets the <see cref="T:System.DateTime" /> of this trace (via <see cref="P:System.DateTime.UtcNow" />).</summary>
        /// <returns>The <see cref="T:System.DateTime" /> of this trace (via <see cref="P:System.DateTime.UtcNow" />).</returns>
        public DateTime Timestamp { get; private set; }
    }
}