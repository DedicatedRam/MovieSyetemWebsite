// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function changeBack() {
    var i = 0;
    var images = ['https://i.pinimg.com/originals/ca/fc/d1/cafcd1270cfe0d27ce714ee04e8e6714.jpg',
        'https://c4.wallpaperflare.com/wallpaper/675/275/718/joker-2019-movie-joker-joaquin-phoenix-actor-men-hd-wallpaper-preview.jpg',
        'https://cdn.wallpapersafari.com/22/80/NBKYi0.jpg',
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-8n8aMtni11TlU-BR8MMYnHPs2D9W5geYfQ&usqp=CAU'];
    var image = $('.backgroundIMG');
    setInterval(function () {
        image.fadeOut(500, function () {
            image.css('background-image', 'url(' + images[i] + ')');
            image.fadeIn(500);
        });
        if (i == (images.length - 1)) {
            i = 0;
        } else {
            i++;
        }
    }, 10000);
})