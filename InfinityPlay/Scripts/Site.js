
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

console.clear();
// Play Icon Switcher
//$("#play-btn").click(function (e) {
//    e.preventDefault();
//    $(this).find("i").toggleClass("fa-play-circle fa-pause-circle");
//});

const songs = [];
const songNames = [];
const artists = [];
const posters = [];

var player = new Audio();
var currentSong = 0;

$(document).ready(function () {
    player.src = song[currentSong];
    document.getElementById("songName").textContent = songNames[currentSong];
    document.getElementById("songArtist").textContent = artists[currentSong];
});



function playTrack(trackId) {
    player.currentSong = 0;
    player.currentTime = 0;
    player.src = '/Audio/' + trackId + '.mp3';
    player.play();
    $("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    songNames[currentSong] = document.getElementById("songName").textContent;
    artists[currentSong] = document.getElementById("songArtist").textContent;
}

function playPause() {
    if (musicTracker == player()) {
        player[currentSong].play();
        musicTracker = player.play();
        $("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    }
    else {
        player[currentSong].pause();
        musicTracker = 'noMusic'
        player.pause();
        player.currentTime = 0;
        player.currentSong = 0;
        $("#play-btn i").removeClass("fa-pause-circle").addClass("fa-play-circle")
    }
};


function playPause() {
    if (player.paused) {
        player.play();
        $("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    }
    else {
        player.pause();
        $("#play-btn i").removeClass("fa-pause-circle").addClass("fa-play-circle")
    }
}


player.addEventListener('timeupdate', function () {
    var position = player.currentTime / player.duration;
    document.getElementById("progressBar").style.width = position * 100 + '%';
    displayTrackTime();
    if (player.ended) {
        next();
    }
});

function displayTrackTime() {
    var stringCurrentTime = getFormatedTime(player.currentTime);
    var stringTotalTime = getFormatedTime(player.duration);
    playbackTime.textContent = stringCurrentTime + ' / ' + stringTotalTime;
}

function getFormatedTime(seconds) {
    if (seconds && !isNaN(seconds)) {
        seconds = Math.round(seconds);
        var min = Math.floor(seconds / 60);
        var sec = seconds % 60;
        min = (min < 10) ? "0" + min : min;
        sec = (sec < 10) ? "0" + sec : sec;
        return min + ":" + sec;
    }
    else {
        return "0:00"
    }
}

function next() {
    currentSong++;
    if (currentSong > songs.length - 1) {
        currentSong = 0;
    }
    playSong();
    //$("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    $("#albumArt img").attr("src", showPoster[currentSong]);
}

function prev() {
    currentSong--;
    if (currentSong < 0) {
        currentSong = songs.lenth - 1;
    }
    playSong();
    //$("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    $("albumArt img").attr("src", showPoster[currentSong]);
}


function toggleVolume() {
    player.muted = !player.muted
    $("#volumeIcon").toggleClass("fa-volume-up").toggleClass("fa-volume-mute");
}

player.addEventListener('ended', function () {
    player.pause();
    player.src = "/Audio/";
    player.load();
    player.play
});

var playlist = 'noMusic';
var playListAudio = [];
$(".tracklist-play-pause").each(function () {
    var load = new Audio($($this).attr("/Audio/"));
    load.load();
    load.addEventListener('ended', function () {
        next();
    });
    playListAudio.push(load);
});

var currentSong = 0;

playPause 
