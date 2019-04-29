
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


//----------------------- Search function
(function () {
    var Search = document.getElementById('Search'),
        input = Search.querySelector('input.Search-input'),
        ctrlClose = Search.querySelector('span.Search-close'),
        isOpen = isAnimating = false,
        // show/hide search area
        toggleSearch = function (evt) {
            // return if open and the input gets focused
            if (evt.type.toLowerCase() === 'focus' && isOpen) return false;

            var offsets = Search.getBoundingClientRect();
            if (isOpen) {
                classie.remove(Search, 'open');

                // trick to hide input text once the search overlay closes 
                // todo: hardcoded times, should be done after transition ends
                if (input.value !== '') {
                    setTimeout(function () {
                        classie.add(Search, 'hideInput');
                        setTimeout(function () {
                            classie.remove(Search, 'hideInput');
                            input.value = '';
                        }, 300);
                    }, 500);
                }

                input.blur();
            }
            else {
                classie.add(Search, 'open');
            }
            isOpen = !isOpen;
        };

    // events
    input.addEventListener('focus', toggleSearch);
    ctrlClose.addEventListener('click', toggleSearch);
    // esc key closes search overlay
    // keyboard navigation events
    document.addEventListener('keydown', function (ev) {
        var keyCode = ev.keyCode || ev.which;
        if (keyCode === 27 && isOpen) {
            toggleSearch(ev);
        }
    });


    /***** for demo purposes only: don't allow to submit the form *****/
    Search.querySelector('button[type="submit"]').addEventListener('click', function (ev) { ev.preventDefault(); });
})();


//----------------------- END: Search function


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

var music = document.getElementById('music'); // id for audio element
var duration = music.duration; // Duration of audio clip, calculated here for embedding purposes
var pButton = document.getElementById('pButton'); // play button
var playhead = document.getElementById('playhead'); // playhead
var timeline = document.getElementById('timeline'); // timeline

// timeline width adjusted for playhead
var timelineWidth = timeline.offsetWidth - playhead.offsetWidth;

// play button event listenter
pButton.addEventListener("click", play);

// timeupdate event listener
music.addEventListener("timeupdate", timeUpdate, false);

// makes timeline clickable
timeline.addEventListener("click", function (event) {
    moveplayhead(event);
    music.currentTime = duration * clickPercent(event);
}, false);

// returns click as decimal (.77) of the total timelineWidth
function clickPercent(event) {
    return (event.clientX - getPosition(timeline)) / timelineWidth;
}

// makes playhead draggable
playhead.addEventListener('mousedown', mouseDown, false);
window.addEventListener('mouseup', mouseUp, false);

// Boolean value so that audio position is updated only when the playhead is released
var onplayhead = false;

// mouseDown EventListener
function mouseDown() {
    onplayhead = true;
    window.addEventListener('mousemove', moveplayhead, true);
    music.removeEventListener('timeupdate', timeUpdate, false);
}

// mouseUp EventListener
// getting input from all mouse clicks
function mouseUp(event) {
    if (onplayhead == true) {
        moveplayhead(event);
        window.removeEventListener('mousemove', moveplayhead, true);
        // change current time
        music.currentTime = duration * clickPercent(event);
        music.addEventListener('timeupdate', timeUpdate, false);
    }
    onplayhead = false;
}
// mousemove EventListener
// Moves playhead as user drags
function moveplayhead(event) {
    var newMargLeft = event.clientX - getPosition(timeline);

    if (newMargLeft >= 0 && newMargLeft <= timelineWidth) {
        playhead.style.marginLeft = newMargLeft + "px";
    }
    if (newMargLeft < 0) {
        playhead.style.marginLeft = "0px";
    }
    if (newMargLeft > timelineWidth) {
        playhead.style.marginLeft = timelineWidth + "px";
    }
}

// timeUpdate
// Synchronizes playhead position with current point in audio
function timeUpdate() {
    var playPercent = timelineWidth * (music.currentTime / duration);
    playhead.style.marginLeft = playPercent + "px";
    if (music.currentTime == duration) {
        pButton.className = "";
        pButton.className = "fas fa-play";
    }
}

//Play and Pause
function play() {
    // start music
    if (music.paused) {
        music.play();
        // remove play, add pause
        pButton.className = "";
        pButton.className = "fas fa-pause";
    } else { // pause music
        music.pause();
        // remove pause, add play
        pButton.className = "";
        pButton.className = "fas fa-play";
    }
}

// Gets audio file duration
music.addEventListener("canplaythrough", function () {
    duration = music.duration;
}, false);

// getPosition
// Returns elements left position relative to top-left of viewport
function getPosition(el) {
    return el.getBoundingClientRect().left;
}




