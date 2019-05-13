
//-------------Navigating the app without hard reloading pages
$(document).ready(function () {
    $('ul#navLinks li').click(function () {
        const page = $(this).attr('data-page-type');
        const url = page;
        window.history.replaceState({}, null, url);
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



var player = new Audio();
var currentSong = 0;

//$(document).ready(function () {
//    player.src = song[currentSong];
//    document.getElementById("songName").textContent = songNames[currentSong];
//    document.getElementById("songArtist").textContent = artists[currentSong];
//});

//var xhr = new XMLHttpRequest();
//const url = 'https://jsonplaceholder.typicode.com/posts';
//xhr.open("GET", url, true );
//xhr.send();

//xhr.onreadystatechange = function () {
//    if (xhr.readyState == 4 && xhr.status == 200) {
//        alert(xhr.responseText);
//    }
//};

//xhr.onload = function () {

//}

//$(document).ready(function () {
//    var tracks = [
//        {
//            trackName: 'The Journey',
//            trackArtist: 'Tom Misch',
//            trackFile: 'E8F45485-EC19-4A0C-93AC-B840D83F8A9D.mp3',
//            trackNumer: '1',
//            trackDuration: '265',
//            trackArt: 'TM-SWL008.jpg'
//        },
//        {
//            trackName: 'Risk',
//            trackArtist: 'Tom Misch',
//            trackFile: '57726A4A-5D8B-4719-A43B-70E6929B4082.mp3',
//            trackNumer: '2',
//            trackDuration: '265',
//            trackArt: 'TM-SWL008.jpg'
//        },
//        {
//            trackName: 'South East',
//            trackArtist: 'Tom Misch',
//            trackFile: '72588257-3D20-4697-8910-CB06698D75E8',
//            trackNumer: '3',
//            trackDuration: '265',
//            trackArt: 'TM-SWL008.jpg'
//        }
//    ]
//});

//for (let track of tracks) {
//    $("#songs").appendd('<div class="row d-flex tracklist-row p-3" role="button" tabindex="0" data-audio="' + tracks.trackFile + '">' +
//        '<div class= "pr-3 mt-2" >' +
//        '<button class="tracklist-play-pause" id="songs" data-audio="' + tracks.trackFile + '" aria-hidden="true">' +
//        '<i class="fas fa-play" aria-hidden="true">' +
//        '</i>' +
//        '</button>' +
//        '</div>' +
//        '< div class="flex-grow-1" >' +
//        '<div class="mt-2 d-block" id="title">' + tracks.trackName + '</div>' +
//        ' <div class="text-muted" id="artist">' + tracks.trackArtist + '</div >' +
//        '</div>' +
//        '<div class="mt-2 track-time" data-duration="' + tracks.trackDuration + '>' +
//        '</div>' +
//        '</div>');
//}

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
    $("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    $("#albumArt img").attr("src", posters[currentSong]);
}

function prev() {
    currentSong--;
    if (currentSong < 0) {
        currentSong = songs.lenth - 1;
    }
    playSong();
    $("#play-btn i").removeClass("fa-play-circle").addClass("fa-pause-circle");
    $("albumArt img").attr("src", posters[currentSong]);
}


function toggleVolume() {
    player.muted = !player.muted
    $("#volumeIcon").toggleClass("fa-volume-up").toggleClass("fa-volume-mute");
}

