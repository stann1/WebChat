﻿/// <reference path="class.js" />
/// <reference path="persister.js" />
/// <reference path="external/jquery-2.0.3.js" />
/// <reference path="ui.js" />

var controllers = (function () {

	var updateTimer = null;

	var rootUrl = "http://localhost:55455/api/";
	var Controller = Class.create({
		init: function () {
			this.persister = persisters.get(rootUrl);
		},
		loadUI: function (selector) {

		    this.loadOnlineUsersUI("#online");

			if (this.persister.isUserLoggedIn()) {
				this.loadChatUI(selector);
			}
			else {
				this.loadLoginFormUI(selector);
			}
			this.attachUIEventHandlers(selector);
		},
		loadLoginFormUI: function (selector) {
			var loginFormHtml = ui.loginForm()
			$(selector).html(loginFormHtml);
		},
		loadChatUI: function (selector) {
			var self = this;
			var gameUIHtml =
				ui.chatUI(this.persister.username());
			$(selector).html(gameUIHtml);

			this.updateUI(selector);

			updateTimer = setInterval(function () {
				self.updateUI(selector);
			}, 15000);
		},
		loadOnlineUsersUI: function(selector)
		{
		    var self = this;
		    $(selector).html("test");
		    self.persister.user.listOnline(
                function(data) {
                    var html = ui.onlineUsers(data);
                    $(selector).html(html);
                }, 
                function(err) {
                    alert(JSON.stringify(err));
                })
		},
		attachUIEventHandlers: function (selector) {
			var wrapper = $(selector);
			var self = this;

			wrapper.on("click", "#btn-show-login", function () {
				wrapper.find(".button.selected").removeClass("selected");
				$(this).addClass("selected");
				wrapper.find("#login-form").show();
				wrapper.find("#register-form").hide();
			});
			wrapper.on("click", "#btn-show-register", function () {
				wrapper.find(".button.selected").removeClass("selected");
				$(this).addClass("selected");
				wrapper.find("#register-form").show();
				wrapper.find("#login-form").hide();
			});

			wrapper.on("click", "#btn-login", function () {
				var user = {
					username: $(selector + " #tb-login-username").val(),
					password: $(selector + " #tb-login-password").val()
				}

				self.persister.user.login(user, function () {
					self.loadChatUI(selector);
				}, function (err) {
					wrapper.find("#error-messages").text(err.responseJSON.Message);
				});
				return false;
			});
			wrapper.on("click", "#btn-register", function () {
				var user = {
					username: $(selector).find("#tb-register-username").val(),
					password: $(selector + " #tb-register-password").val()
				}
				self.persister.user.register(user, function () {
				    self.loadChatUI(selector);
				}, function (err) {
					console.log(err.responseJSON.Message);
				});
				return false;
			});
			wrapper.on("click", "#btn-logout", function () {
				self.persister.user.logout(function () {
					self.loadLoginFormUI(selector);
					clearInterval(updateTimer);
				}, function (err) {
				});
			});

			wrapper.on("click", "#btn-start", function () {
				var chatWindow = {
					title: $("#tb-create-title").val(),
					number: $("#tb-create-number").val(),
				}
				var password = $("#tb-create-pass").val();
				if (password) {
					chatWindow.password = password;
				}
				self.persister.chat.create(chatWindow);
			});

			wrapper.on("click", "#active-games-container li.game-status-full a.btn-active-game", function () {
				var gameCreator = $(this).parent().data("creator");
				var myNickname = self.persister.username();
				if (gameCreator == myNickname) {
					$("#btn-game-start").remove();
					var html =
						'<a href="#" id="btn-game-start">' +
							'Start' +
						'</a>';
					$(this).parent().append(html);
				}
			});

			wrapper.on("click", "#btn-game-start", function () {
				var parent = $(this).parent();

				var gameId = parent.data("game-id");
				self.persister.chat.start(gameId, function () {
					wrapper.find("#game-holder").html("started");
				},
				function (err) {
					alert(JSON.stringify(err));
				});

				return false;
			});
			
			wrapper.on("click", ".active-games .in-progress", function () {
				self.loadGame(selector, $(this).parent().data("game-id"));
			});
		},
		updateUI: function (selector) {
			//this.persister.chat.open(function (games) {
			//	var list = ui.openGamesList(games);
			//	$(selector + " #open-games")
			//		.html(list);
			//});
			//this.persister.chat.myActive(function (games) {
			//	var list = ui.activeGamesList(games);
			//	$(selector + " #active-games")
			//		.html(list);
			//});
			//this.persister.message.all(function (msg) {
			//	var msgList = ui.messagesList(msg);
			//	$(selector + " #messages-holder").html(msgList);
			//});
		}
	});
	return {
		get: function () {
			return new Controller();
		}
	}
}());

$(function () {
	var controller = controllers.get();
	controller.loadUI("#content");
});