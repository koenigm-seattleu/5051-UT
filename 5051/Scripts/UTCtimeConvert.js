// Copyright All Rights Reserved
// Code provided by Mike Koenig for Seattle University Based Senior Projects
// All other usage of this code is prohibited without writen premission from Mike Koenig

$(function () {
    setTimezoneCookie();
});

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires + "; path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === " ") c = c.substring(1);
        if (c.indexOf(name) === 0) return c.substring(name.length, c.length);
    }
    return "";
}

function deleteCookie(cname) {
    var d = new Date();
    d.setTime(d.getTime() + (-1 * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + "" + "; " + expires;
}

function setTimezoneCookie() {

    var timezoneCookie = "timezoneoffset";
    var myValue;
    var myExpiration = 20;

    // if the timezone cookie does not exists create one.
    var myCookieValue = getCookie(timezoneCookie);
    if (myCookieValue === "") {

        // No cookie, so try to set one.

        // check if the browser supports cookie
        var testCookie = "testcookietimezoneCookie";
        setCookie(testCookie, "1", myExpiration);
        myCookieValue = getCookie(testCookie);

        if (myCookieValue !== "") {
            //Supports Cookies

            // delete the test cookie
            deleteCookie(testCookie);

            // create a new cookie
            myValue = new Date().getTimezoneOffset();

            setCookie(timezoneCookie, myValue, myExpiration);

            // re-load the page
            location.reload();
        }
    }
    else {
        // Has cookie.
        // if the current timezone and the one stored in cookie are different
        // then store the new timezone in the cookie and refresh the page.

        var storedOffset = parseInt(myCookieValue);
        var currentOffset = new Date().getTimezoneOffset();

        // user may have changed the timezone
        if (storedOffset !== currentOffset) {

            myValue = new Date().getTimezoneOffset();

            setCookie(timezoneCookie, myValue, myExpiration);

            location.reload();
        }
    }
}
