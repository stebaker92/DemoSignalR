﻿"use strict";

var loginToken = null;
var connection

fetch("/token").then(response => {
    response.json().then(token => {
        console.log("got token", token);
        loginToken = token.token;

        initHub();
    });
});

function initHub() {
    // https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-2.1#bearer-token-authentication
    connection = new signalR.HubConnectionBuilder()
        //.withUrl("/chatHub")
        .withUrl("/chatHub", {
            accessTokenFactory: () => {
                console.log("factory requested token", loginToken);
                return loginToken;
            }
        })
        .build();

    connection.on("ReceiveMessage", function (user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("StatusUpdated", function (status) {

        console.log("Status updated to ", status);

        var encodedMsg = "Your status has been updated to " + status;

        var li = document.createElement("li");

        li.textContent = encodedMsg;

        document.getElementById("messagesList").appendChild(li);
    });
    connection.start().catch(function (err) {
        return console.error(err.toString());
    });
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
