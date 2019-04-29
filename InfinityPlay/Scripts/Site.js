
//-------------Navigating the app without hard reloading pages
$(document).ready(function () {
    $('ul#navLinks li').click(function () {
        const url = $(this).attr('data-page-type');
        history.pushState(null, null, url);
        $('#mainContent').load("/partial/" + url, "", function () {
            Grade(document.querySelectorAll('.gradient-wrap'));
        });
    });
});
//------------- END: Navigating the app without hard reloading pages


//--------------------- Gradient Average -------------
window.addEventListener('load', function () {
    Grade(document.querySelectorAll('.gradient-wrap'));
});
//---------------------- END: Gradient Average -----------


//---------------------- Toggle Responsive Now Playing Bar -----------
//$(document).ready(function () {
//    $("button.slide").click(function () {
//        $("div.Root__now-playing-bar.slider").slideUp();
//        debugger
//    });
//    $("button.slide").click(function () {
//        $("div.Root__now-playing-bar.slider").slideDown();
//    });
//});
//---------------------- END:Toggle Responsive Now Playing Bar -----------



// ----------------------- Media Player ----------------------------------

window.onload = function () {

    var myAudio = document.getElementById('my-audio');
    var play = document.getElementById('play');
    var pause = document.getElementById('pause');

    // associate functions with the 'onclick' events
    play.onclick = playAudio;
    pause.onclick = pauseAudio;

    function playAudio() {
        myAudio.play();
    }

    function pauseAudio() {
        myAudio.pause();
    }

}