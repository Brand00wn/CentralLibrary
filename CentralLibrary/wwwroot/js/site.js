// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    function isValidDateFormat(dateString) {
        const regex = /^\d{4}-\d{2}-\d{2}$/;

        return regex.test(dateString);
    }

    $('#getGoogleBooksData').click(function () {
        fetchGoogleBooksData();
    });

    async function fetchGoogleBooksData() {
        var title = $("#BookRegistration_Title").val().replace(/\s/g, '+');
        const apiUrl = `https://www.googleapis.com/books/v1/volumes?q=${title}`;

        const xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                const data = JSON.parse(this.responseText);

                var bookInfos = null;
                var books = data.items;

                for (let i = 0; i < books.length; i++) {
                    if (books[i].volumeInfo.hasOwnProperty("title") && books[i].volumeInfo.hasOwnProperty("authors") && books[i].volumeInfo.hasOwnProperty("categories") && (books[i].volumeInfo.hasOwnProperty("pageCount") && books[i].volumeInfo.pageCount > 0) && (books[i].volumeInfo.hasOwnProperty("publishedDate")) && isValidDateFormat(books[i].volumeInfo.publishedDate) && books[i].volumeInfo.hasOwnProperty("imageLinks")) {
                        bookInfos = books[i]. volumeInfo;
                        break;
                    }
                }

                console.log(bookInfos);

                $("#BookRegistration_Title").val(bookInfos.title);
                $("#BookRegistration_Author").val(bookInfos.authors[0]);
                $("#BookRegistration_ISBN").val(bookInfos.industryIdentifiers ? bookInfos.industryIdentifiers[0].identifier : "");
                $("#BookRegistration_Genre").val(bookInfos.categories[0]);
                $("#BookRegistration_Pages").val(bookInfos.pageCount);
                $("#BookRegistration_PublicationDate").val(bookInfos.publishedDate);
                $("#BookRegistration_Summary").val(bookInfos.description);
                $("#BookRegistration_ImageUrl").val(bookInfos.imageLinks.thumbnail);
                $(".bookImageRegistration").attr("src", bookInfos.imageLinks.thumbnail);

            }
        };
        xhttp.open("GET", apiUrl, true);
        xhttp.send();
    }
});

(function () {
    const win = window
    const doc = document.documentElement

    doc.classList.remove('no-js')
    doc.classList.add('js')

    // Reveal animations
    if (document.body.classList.contains('has-animations')) {
        /* global ScrollReveal */
        const sr = window.sr = ScrollReveal()

        sr.reveal('.feature, .pricing-table-inner', {
            duration: 600,
            distance: '20px',
            easing: 'cubic-bezier(0.5, -0.01, 0, 1.005)',
            origin: 'bottom',
            interval: 100
        })

        doc.classList.add('anime-ready')
        /* global anime */
        anime.timeline({
            targets: '.hero-figure-box-05'
        }).add({
            duration: 400,
            easing: 'easeInOutExpo',
            scaleX: [0.05, 0.05],
            scaleY: [0, 1],
            perspective: '500px',
            delay: anime.random(0, 400)
        }).add({
            duration: 400,
            easing: 'easeInOutExpo',
            scaleX: 1
        }).add({
            duration: 800,
            rotateY: '-15deg',
            rotateX: '8deg',
            rotateZ: '-1deg'
        })

        anime.timeline({
            targets: '.hero-figure-box-06, .hero-figure-box-07'
        }).add({
            duration: 400,
            easing: 'easeInOutExpo',
            scaleX: [0.05, 0.05],
            scaleY: [0, 1],
            perspective: '500px',
            delay: anime.random(0, 400)
        }).add({
            duration: 400,
            easing: 'easeInOutExpo',
            scaleX: 1
        }).add({
            duration: 800,
            rotateZ: '20deg'
        })

        anime({
            targets: '.hero-figure-box-01, .hero-figure-box-02, .hero-figure-box-03, .hero-figure-box-04, .hero-figure-box-08, .hero-figure-box-09, .hero-figure-box-10',
            duration: anime.random(600, 800),
            delay: anime.random(600, 800),
            rotate: [anime.random(-360, 360), function (el) { return el.getAttribute('data-rotation') }],
            scale: [0.7, 1],
            opacity: [0, 1],
            easing: 'easeInOutExpo'
        })
    }
}())
