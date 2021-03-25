// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    moment.locale("bg");
    $("time").each(function (i, e) {
        const dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("llll"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});

function ConfirmDelete(confirmMessage) {
    if (!confirm(confirmMessage)) {
        event.preventDefault();
    }
}

function ToggleAddWorkoutDayInputForm(toggleElement) {
    $(toggleElement).toggle();
}

// Set "nav-link-active" to current active links from navbar menu:
$(document).ready(function () {
    let currentLocation = location.href;
    let navBarUl = (document.getElementsByClassName("navbar-nav"));

    // "Login" and "Register" are not in the same ".navbar-nav" element as other navbar links!
    for (let i = 0; i < navBarUl.length; i++) {
        let links = navBarUl[i].querySelectorAll("a");

        SetActiveClassToElement(currentLocation, links, "nav-link-active");
    }
});

function SetActiveClassToElement(locationHref, links, className) {
    for (let i = 0; i < links.length; i++) {
        let currentElement = links[i];

        if (currentElement.href === locationHref) {

            while (currentElement.tagName.toLowerCase() !== "li") {
                currentElement = currentElement.parentNode;
            }

            currentElement.children[0].classList.add(className);
            break;
        }
    }
}