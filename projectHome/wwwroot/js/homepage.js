
var loopVar = true; //var to determine looping based on day of week
var dayOfWeek = new Date().getDay();

//set loop to false for weekends
if (dayOfWeek === 0 || dayOfWeek === 6) {
    loopVar = false;
}

/*------- Swiper Slider -------*/
var swiper = new Swiper('.swiper-container', {
    pagination: {
        el: '.swiper-pagination',
        type: 'bullets',
        clickable: true
    },
    paginationClickable: true,
    centeredSlides: true,
    autoplay: {
        delay: 3000
    },
    speed: 1500,
    //disable loop if play video only
    loop: loopVar,
    autoplayDisableOnInteraction: false,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    }
});