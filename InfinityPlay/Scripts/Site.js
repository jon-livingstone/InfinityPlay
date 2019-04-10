//Change page content dynamically without refreshing the page.
function myFunction() {
    location.replace("")
}

$("#navBar-Group-Links a").on('click', function (e) {
    e.stopPropagation(); // prevents the link from actually opening the target
    url = $(e.target).attr('href');

    $.get(url)
        .done(function (response) {
            $(".main-panel").cshtml(response);
        })
        .fail(function () {
            $(".main-panel").prepend("SHIT BROKE!");
        })
});

});