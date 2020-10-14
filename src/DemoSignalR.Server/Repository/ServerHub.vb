﻿Imports DemoSignalR.Model
Imports Microsoft.AspNet.SignalR

Public Class ServerHub
    Inherits Hub

    Public Shared Property iListMessage As List(Of Messages) = New List(Of Messages)

    Public Sub ClientChat(ByVal messages As Messages)
        Clients.All.RefreshData(messages)
    End Sub

    Public Sub ServerChat(ByVal messages As Messages)
        If (Not IsNothing(messages) AndAlso Not IsNothing(messages.Server) AndAlso messages.Server.ServerId <> "") Then
            Clients.Client(messages.Client.ClientId).RefreshData(messages)
        Else
            Clients.All.RefreshData(messages)
        End If
    End Sub

    Public Overrides Function OnConnected() As Task
        Dim messages As New Messages With {
            .Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
            .Client = New DemoSignalR.Model.Client With {.ClientId = Context.ConnectionId, .NickName = "Unknow"},
            .Message = "",
            .NickName = "Unknow"}
        iListMessage.Add(messages)
        [Public].MainForm.WriteToConsole(False, "Client connected: " & Context.ConnectionId)
        Return MyBase.OnConnected()
    End Function

    Public Overrides Function OnDisconnected(stopCalled As Boolean) As Task
        iListMessage.RemoveAll(Function(x) x.ClientId.Equals(Context.ConnectionId))
        [Public].MainForm.WriteToConsole(False, "Client disconnected: " & Context.ConnectionId)

        Return MyBase.OnDisconnected(stopCalled)
    End Function
End Class