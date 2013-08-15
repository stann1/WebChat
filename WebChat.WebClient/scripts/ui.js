var ui = (function () {

	function buildLoginForm() {
		var html =
            '<div id="login-form-holder">' +
				'<form>' +
					'<div id="login-form">' +
						'<label for="tb-login-username">Username: </label>' +
						'<input type="text" id="tb-login-username"><br />' +
						'<label for="tb-login-password">Password: </label>' +
						'<input type="text" id="tb-login-password"><br />' +
						'<button id="btn-login" class="button">Login</button>' +
					'</div>' +
					'<div id="register-form" style="display: none">' +
						'<label for="tb-register-username">Username: </label>' +
						'<input type="text" id="tb-register-username"><br />' +
						'<label for="tb-register-password">Password: </label>' +
						'<input type="text" id="tb-register-password"><br />' +
						'<button id="btn-register" class="button">Register</button>' +
					'</div>' +
					'<a href="#" id="btn-show-login" class="button selected">Login</a>' +
					'<a href="#" id="btn-show-register" class="button">Register</a>' +
				'</form>' +
				'<div id="error-messages"></div>' +
            '</div>';
		return html;
	}

	function buildChatUI(username) {
	    var html = '<div id="wrappar">' +
        '<div id="header">' +
            '<img id="userPicture" src="userPicture.jpg" />' +
            '<div id="userDescription">tova e testovo opisanie</div>' +
        '</div>' +
        '<div id="content">' +
            '<div id="users">' +
                '<div class="onlineUsers">' +
                    '<img class="picture" src="picture.jpg"/>' +
                    '<div class="description">dfsdfsdfsd</div>' +
                '</div>                                      ' +
                '<div class="onlineUsers">                   ' +
                '    <img class="picture" src="picture.jpg"/>' +
                '    <div class="description">fsdfdsfs</div> ' +
                '</div>                                      ' +
                '<div class="onlineUsers">                   ' +
                '    <img class="picture" src="picture.jpg"/>' +
                '    <div class="description">fsfsdfsd</div> ' +
                '</div>                                      ' +
                '<div class="onlineUsers">                   ' +
                '    <img class="picture" src="picture.jpg"/>' +
                '    <div class="description">fsdfsd</div>   ' +
                '</div>                                      ' +
                '<div class="onlineUsers">                   ' +
                '    <img class="picture" src="picture.jpg"/>' +
                '    <div class="description">fasdfds</div>  ' +
                '</div>                                      ' +
            '</div>                                         ' +
            '<div id="chat">                                ' +
            '    <div id="ChatContent"></div>               ' +
            '    <div id="inputContent">                    ' +
            '        <input id="input" type="text" />       ' +
            '        <button id="sendMessage">Send</button> ' +
            '        <button id="sendFile">File</button>    ' +
            '    </div>' +
            '</div>' +
        '</div>' +
    '</div>';
		return html;
	}
	
	function buildMessagesList(messages) {
		var list = '<ul class="messages-list">';
		var msg;
		for (var i = 0; i < messages.length; i += 1) {
			msg = messages[i];
			var item =
				'<li>' +
					'<a href="#" class="message-state-' + msg.username + '">' +
                       msg.username +' --> ' +
						msg.text +
					'</a>' +
				'</li>';
			list += item;
		}
		list += '</ul>';
		return list;
	}

	function buildOnlineList(users) {
	    var html = '<div>' + 
	            '<ul>';

	    var usersCount = users.length;

	    for (var i = 0; i < usersCount; i++) {
	        html += '<li data-id='+ users[i].Id + '>' +
                users[i].UserName + '</li>';
	    }

	    html += '</ul>';

	    return html;

	}

	return {
		chatUI: buildChatUI,
		loginForm: buildLoginForm,
        onlineUsers: buildOnlineList,
		messagesList: buildMessagesList
	}

}());