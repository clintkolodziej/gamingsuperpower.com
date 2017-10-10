$(function () {

    // addLoadEvent(preloader);

    $("#scroller").click(function (e){
		e.preventDefault();
        $('html, body').animate({
            scrollTop: $($("#scroller").attr('href')).offset().top
        }, 800);
    });

});

// function preloader() {

// 	if (document.images) {

// 		var img1 = new Image();
// 		var img2 = new Image();
// 		var img3 = new Image();
//         var img4 = new Image();

// 		img1.src = "/images/image-1.jpg";
// 		img2.src = "/images/image-2.jpg";
// 		img3.src = "/images/image-3.jpg";
//         img4.src = "/images/image-4.jpg";

// 	}
// }

// function addLoadEvent(func) {
// 	var oldonload = window.onload;
// 	if (typeof window.onload != 'function') {
// 		window.onload = func;
// 	} else {
// 		window.onload = function() {
// 			if (oldonload) {
// 				oldonload();
// 			}
// 			func();
// 		}
// 	}
// }