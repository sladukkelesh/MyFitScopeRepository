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

$(document).ready(function () {
    let currentLocation = location.href;
    let navBarUl = (document.getElementsByClassName("navbar-nav flex-grow-1"))[0];
    let links = navBarUl.querySelectorAll("a");

    for (let i = 0; i < links.length; i++) {
        if (links[i].href === currentLocation) {
            let currentElement = links[i];

            while (currentElement.tagName.toLowerCase() !== "li") {
                currentElement = currentElement.parentNode;
            }

            currentElement.children[0].classList.add("nav-link-active");
            break;
        }
    }
});