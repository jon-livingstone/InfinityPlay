
//-------------Navigating the app without hard reloading pages
$(document).ready(function () {
    $('ul#navLinks li').click(function () {
        const url = $(this).attr('data-page-type');
        history.pushState(null, null, url);
        $('.content-spacing').load("/partial/"+ url);
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

function inputfocus () {
    var x = document.getElementById("input.Search-input").autofocus;
}

//----------------------- END: Search function


//--------------------- Gradient Average -------------
window.addEventListener('load', function () {
    /*
        A NodeList of all your image containers (Or a single Node).
        The library will locate an <img /> within each
        container to create the gradient from.
     */
    Grade(document.querySelectorAll('.gradient-wrap'))
})
//---------------------- END: Gradient Average-----------
