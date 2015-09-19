﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18408.
// 
#pragma warning disable 1591

namespace Myfashionmarketer.Api.Drafts {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DraftsSoap", Namespace="http://tempuri.org/")]
    public partial class Drafts : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetDraftsByUserIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddDraftOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateDraftsOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteDraftsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDraftMessageByUserIdAndGroupIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateDraftsMessageOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Drafts() {
            this.Url = global::Myfashionmarketer.Properties.Settings.Default.Myfashionmarketer_Api_Drafts_Drafts;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetDraftsByUserIdCompletedEventHandler GetDraftsByUserIdCompleted;
        
        /// <remarks/>
        public event AddDraftCompletedEventHandler AddDraftCompleted;
        
        /// <remarks/>
        public event UpdateDraftsCompletedEventHandler UpdateDraftsCompleted;
        
        /// <remarks/>
        public event DeleteDraftsCompletedEventHandler DeleteDraftsCompleted;
        
        /// <remarks/>
        public event GetDraftMessageByUserIdAndGroupIdCompletedEventHandler GetDraftMessageByUserIdAndGroupIdCompleted;
        
        /// <remarks/>
        public event UpdateDraftsMessageCompletedEventHandler UpdateDraftsMessageCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetDraftsByUserId", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetDraftsByUserId(string UserId) {
            object[] results = this.Invoke("GetDraftsByUserId", new object[] {
                        UserId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetDraftsByUserIdAsync(string UserId) {
            this.GetDraftsByUserIdAsync(UserId, null);
        }
        
        /// <remarks/>
        public void GetDraftsByUserIdAsync(string UserId, object userState) {
            if ((this.GetDraftsByUserIdOperationCompleted == null)) {
                this.GetDraftsByUserIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDraftsByUserIdOperationCompleted);
            }
            this.InvokeAsync("GetDraftsByUserId", new object[] {
                        UserId}, this.GetDraftsByUserIdOperationCompleted, userState);
        }
        
        private void OnGetDraftsByUserIdOperationCompleted(object arg) {
            if ((this.GetDraftsByUserIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDraftsByUserIdCompleted(this, new GetDraftsByUserIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddDraft", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string AddDraft(string UserId, string groupid, System.DateTime ModifiedDate, string Message) {
            object[] results = this.Invoke("AddDraft", new object[] {
                        UserId,
                        groupid,
                        ModifiedDate,
                        Message});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void AddDraftAsync(string UserId, string groupid, System.DateTime ModifiedDate, string Message) {
            this.AddDraftAsync(UserId, groupid, ModifiedDate, Message, null);
        }
        
        /// <remarks/>
        public void AddDraftAsync(string UserId, string groupid, System.DateTime ModifiedDate, string Message, object userState) {
            if ((this.AddDraftOperationCompleted == null)) {
                this.AddDraftOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddDraftOperationCompleted);
            }
            this.InvokeAsync("AddDraft", new object[] {
                        UserId,
                        groupid,
                        ModifiedDate,
                        Message}, this.AddDraftOperationCompleted, userState);
        }
        
        private void OnAddDraftOperationCompleted(object arg) {
            if ((this.AddDraftCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddDraftCompleted(this, new AddDraftCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateDrafts", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UpdateDrafts(string Id, string message) {
            object[] results = this.Invoke("UpdateDrafts", new object[] {
                        Id,
                        message});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateDraftsAsync(string Id, string message) {
            this.UpdateDraftsAsync(Id, message, null);
        }
        
        /// <remarks/>
        public void UpdateDraftsAsync(string Id, string message, object userState) {
            if ((this.UpdateDraftsOperationCompleted == null)) {
                this.UpdateDraftsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateDraftsOperationCompleted);
            }
            this.InvokeAsync("UpdateDrafts", new object[] {
                        Id,
                        message}, this.UpdateDraftsOperationCompleted, userState);
        }
        
        private void OnUpdateDraftsOperationCompleted(object arg) {
            if ((this.UpdateDraftsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateDraftsCompleted(this, new UpdateDraftsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteDrafts", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DeleteDrafts(string Id) {
            object[] results = this.Invoke("DeleteDrafts", new object[] {
                        Id});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteDraftsAsync(string Id) {
            this.DeleteDraftsAsync(Id, null);
        }
        
        /// <remarks/>
        public void DeleteDraftsAsync(string Id, object userState) {
            if ((this.DeleteDraftsOperationCompleted == null)) {
                this.DeleteDraftsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteDraftsOperationCompleted);
            }
            this.InvokeAsync("DeleteDrafts", new object[] {
                        Id}, this.DeleteDraftsOperationCompleted, userState);
        }
        
        private void OnDeleteDraftsOperationCompleted(object arg) {
            if ((this.DeleteDraftsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteDraftsCompleted(this, new DeleteDraftsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetDraftMessageByUserIdAndGroupId", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetDraftMessageByUserIdAndGroupId(string UserId, string GroupId) {
            object[] results = this.Invoke("GetDraftMessageByUserIdAndGroupId", new object[] {
                        UserId,
                        GroupId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetDraftMessageByUserIdAndGroupIdAsync(string UserId, string GroupId) {
            this.GetDraftMessageByUserIdAndGroupIdAsync(UserId, GroupId, null);
        }
        
        /// <remarks/>
        public void GetDraftMessageByUserIdAndGroupIdAsync(string UserId, string GroupId, object userState) {
            if ((this.GetDraftMessageByUserIdAndGroupIdOperationCompleted == null)) {
                this.GetDraftMessageByUserIdAndGroupIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDraftMessageByUserIdAndGroupIdOperationCompleted);
            }
            this.InvokeAsync("GetDraftMessageByUserIdAndGroupId", new object[] {
                        UserId,
                        GroupId}, this.GetDraftMessageByUserIdAndGroupIdOperationCompleted, userState);
        }
        
        private void OnGetDraftMessageByUserIdAndGroupIdOperationCompleted(object arg) {
            if ((this.GetDraftMessageByUserIdAndGroupIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDraftMessageByUserIdAndGroupIdCompleted(this, new GetDraftMessageByUserIdAndGroupIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateDraftsMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UpdateDraftsMessage(string draftId, string userid, string groupid, string message) {
            object[] results = this.Invoke("UpdateDraftsMessage", new object[] {
                        draftId,
                        userid,
                        groupid,
                        message});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateDraftsMessageAsync(string draftId, string userid, string groupid, string message) {
            this.UpdateDraftsMessageAsync(draftId, userid, groupid, message, null);
        }
        
        /// <remarks/>
        public void UpdateDraftsMessageAsync(string draftId, string userid, string groupid, string message, object userState) {
            if ((this.UpdateDraftsMessageOperationCompleted == null)) {
                this.UpdateDraftsMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateDraftsMessageOperationCompleted);
            }
            this.InvokeAsync("UpdateDraftsMessage", new object[] {
                        draftId,
                        userid,
                        groupid,
                        message}, this.UpdateDraftsMessageOperationCompleted, userState);
        }
        
        private void OnUpdateDraftsMessageOperationCompleted(object arg) {
            if ((this.UpdateDraftsMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateDraftsMessageCompleted(this, new UpdateDraftsMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetDraftsByUserIdCompletedEventHandler(object sender, GetDraftsByUserIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDraftsByUserIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDraftsByUserIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void AddDraftCompletedEventHandler(object sender, AddDraftCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddDraftCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddDraftCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void UpdateDraftsCompletedEventHandler(object sender, UpdateDraftsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateDraftsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateDraftsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void DeleteDraftsCompletedEventHandler(object sender, DeleteDraftsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteDraftsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteDraftsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetDraftMessageByUserIdAndGroupIdCompletedEventHandler(object sender, GetDraftMessageByUserIdAndGroupIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDraftMessageByUserIdAndGroupIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDraftMessageByUserIdAndGroupIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void UpdateDraftsMessageCompletedEventHandler(object sender, UpdateDraftsMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateDraftsMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateDraftsMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591