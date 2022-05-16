"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//关闭发送按钮，直到建立连接
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    //我们可以将用户提供的字符串分配给元素的textContent，因为它
    //不被解释为标记。如果你以其他方式分配，你
    //应该知道可能的脚本注入问题。
    li.textContent = `${user} says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});