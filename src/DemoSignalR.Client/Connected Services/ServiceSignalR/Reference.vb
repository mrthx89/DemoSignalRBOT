﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace ServiceSignalR
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="ServiceSignalR.ServiceMainSoap")>  _
    Public Interface ServiceMainSoap
        
        'CODEGEN: Generating message contract since element name Phone from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SendWAAPI", ReplyAction:="*")>  _
        Function SendWAAPI(ByVal request As ServiceSignalR.SendWAAPIRequest) As ServiceSignalR.SendWAAPIResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SendWAAPI", ReplyAction:="*")>  _
        Function SendWAAPIAsync(ByVal request As ServiceSignalR.SendWAAPIRequest) As System.Threading.Tasks.Task(Of ServiceSignalR.SendWAAPIResponse)
        
        'CODEGEN: Generating message contract since element name Message from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SendWAAPIModel", ReplyAction:="*")>  _
        Function SendWAAPIModel(ByVal request As ServiceSignalR.SendWAAPIModelRequest) As ServiceSignalR.SendWAAPIModelResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SendWAAPIModel", ReplyAction:="*")>  _
        Function SendWAAPIModelAsync(ByVal request As ServiceSignalR.SendWAAPIModelRequest) As System.Threading.Tasks.Task(Of ServiceSignalR.SendWAAPIModelResponse)
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SendWAAPIRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SendWAAPI", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As ServiceSignalR.SendWAAPIRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As ServiceSignalR.SendWAAPIRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SendWAAPIRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public Phone As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public Message As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Phone As String, ByVal Message As String)
            MyBase.New
            Me.Phone = Phone
            Me.Message = Message
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SendWAAPIResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SendWAAPIResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As ServiceSignalR.SendWAAPIResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As ServiceSignalR.SendWAAPIResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute()>  _
    Partial Public Class SendWAAPIResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SendWAAPIModelRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SendWAAPIModel", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As ServiceSignalR.SendWAAPIModelRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As ServiceSignalR.SendWAAPIModelRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SendWAAPIModelRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public Message As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Message As String)
            MyBase.New
            Me.Message = Message
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SendWAAPIModelResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SendWAAPIModelResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As ServiceSignalR.SendWAAPIModelResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As ServiceSignalR.SendWAAPIModelResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute()>  _
    Partial Public Class SendWAAPIModelResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ServiceMainSoapChannel
        Inherits ServiceSignalR.ServiceMainSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class ServiceMainSoapClient
        Inherits System.ServiceModel.ClientBase(Of ServiceSignalR.ServiceMainSoap)
        Implements ServiceSignalR.ServiceMainSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function ServiceSignalR_ServiceMainSoap_SendWAAPI(ByVal request As ServiceSignalR.SendWAAPIRequest) As ServiceSignalR.SendWAAPIResponse Implements ServiceSignalR.ServiceMainSoap.SendWAAPI
            Return MyBase.Channel.SendWAAPI(request)
        End Function
        
        Public Sub SendWAAPI(ByVal Phone As String, ByVal Message As String)
            Dim inValue As ServiceSignalR.SendWAAPIRequest = New ServiceSignalR.SendWAAPIRequest()
            inValue.Body = New ServiceSignalR.SendWAAPIRequestBody()
            inValue.Body.Phone = Phone
            inValue.Body.Message = Message
            Dim retVal As ServiceSignalR.SendWAAPIResponse = CType(Me,ServiceSignalR.ServiceMainSoap).SendWAAPI(inValue)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function ServiceSignalR_ServiceMainSoap_SendWAAPIAsync(ByVal request As ServiceSignalR.SendWAAPIRequest) As System.Threading.Tasks.Task(Of ServiceSignalR.SendWAAPIResponse) Implements ServiceSignalR.ServiceMainSoap.SendWAAPIAsync
            Return MyBase.Channel.SendWAAPIAsync(request)
        End Function
        
        Public Function SendWAAPIAsync(ByVal Phone As String, ByVal Message As String) As System.Threading.Tasks.Task(Of ServiceSignalR.SendWAAPIResponse)
            Dim inValue As ServiceSignalR.SendWAAPIRequest = New ServiceSignalR.SendWAAPIRequest()
            inValue.Body = New ServiceSignalR.SendWAAPIRequestBody()
            inValue.Body.Phone = Phone
            inValue.Body.Message = Message
            Return CType(Me,ServiceSignalR.ServiceMainSoap).SendWAAPIAsync(inValue)
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function ServiceSignalR_ServiceMainSoap_SendWAAPIModel(ByVal request As ServiceSignalR.SendWAAPIModelRequest) As ServiceSignalR.SendWAAPIModelResponse Implements ServiceSignalR.ServiceMainSoap.SendWAAPIModel
            Return MyBase.Channel.SendWAAPIModel(request)
        End Function
        
        Public Sub SendWAAPIModel(ByVal Message As String)
            Dim inValue As ServiceSignalR.SendWAAPIModelRequest = New ServiceSignalR.SendWAAPIModelRequest()
            inValue.Body = New ServiceSignalR.SendWAAPIModelRequestBody()
            inValue.Body.Message = Message
            Dim retVal As ServiceSignalR.SendWAAPIModelResponse = CType(Me,ServiceSignalR.ServiceMainSoap).SendWAAPIModel(inValue)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function ServiceSignalR_ServiceMainSoap_SendWAAPIModelAsync(ByVal request As ServiceSignalR.SendWAAPIModelRequest) As System.Threading.Tasks.Task(Of ServiceSignalR.SendWAAPIModelResponse) Implements ServiceSignalR.ServiceMainSoap.SendWAAPIModelAsync
            Return MyBase.Channel.SendWAAPIModelAsync(request)
        End Function
        
        Public Function SendWAAPIModelAsync(ByVal Message As String) As System.Threading.Tasks.Task(Of ServiceSignalR.SendWAAPIModelResponse)
            Dim inValue As ServiceSignalR.SendWAAPIModelRequest = New ServiceSignalR.SendWAAPIModelRequest()
            inValue.Body = New ServiceSignalR.SendWAAPIModelRequestBody()
            inValue.Body.Message = Message
            Return CType(Me,ServiceSignalR.ServiceMainSoap).SendWAAPIModelAsync(inValue)
        End Function
    End Class
End Namespace