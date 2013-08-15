/// <reference path="external/jquery.ui.datepicker.js" />
/// <reference path="external/jquery-2.0.3.js" />
/// <reference path="url.js" />

// manage data
// same code is for songs and artists
// with the appropriate checks for existing constraints

var url = getUrl(0);
var numberOfItems = 0;
self = this;

var getAlbum = function (success, error) {
    httpRequester.getJSON(url, function (data) {
        success(data);
    }, error);
}

var readData = function () {
    getAlbum(function (data) {
        $('tr').remove();
        PrintAlbums(data);
        numberOfItems = data.length;
    }, function (err) {
        var errorMsg = JSON.parse(err.responseText);
        self.msgLogger.html(errorMsg.Message);
    });
    return false;
}

var deleteAlbum = function (id, success, error) {
    httpRequester.deleteJSON(getUrl() + id, function (data) {
        success(data);
    }, error);
}

var removeAlbum = function (id) {
    deleteAlbum(id, function () {
        readData();
    }, function (err) {
        var errorMsg = JSON.parse(err.responseText);
        self.msgLogger.html(errorMsg.Message);
    });
    return false;
}

var createAlbum = function (albumData, success, error) {
    httpRequester.postJSON(url, albumData, function (data) {
        success(data);
    }, error);
}

var addAlbum = function (title, date, producer) {
    var albumData = {
        Title: title,
        ReleaseDate: $.datepicker.parseDate('dd.mm.yy', date),
        Producer: producer
    };
    createAlbum(albumData, function () {
    }, function (err) {
        var errorMsg = JSON.parse(err.responseText);
        self.msgLogger.html(errorMsg.Message);
    });
    return false;
}
