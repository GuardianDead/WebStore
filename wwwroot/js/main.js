jQuery.event.special.touchstart = {
    setup: function( _, ns, handle ) {
        this.addEventListener("touchstart", handle, { passive: !ns.includes("noPreventDefault") });
    }
};
jQuery.event.special.touchmove = {
    setup: function( _, ns, handle ) {
        this.addEventListener("touchmove", handle, { passive: !ns.includes("noPreventDefault") });
    }
};
jQuery.event.special.wheel = {
    setup: function( _, ns, handle ){
        this.addEventListener("wheel", handle, { passive: true });
    }
};
jQuery.event.special.mousewheel = {
    setup: function( _, ns, handle ){
        this.addEventListener("mousewheel", handle, { passive: true });
    }
};

$( () => {

	if ( $('.owl-2').length > 0 ) {
        $('.owl-2').owlCarousel({
            center: true,
            items: 1,
            loop: true,
            stagePadding: 5,
            margin: 20,
            smartSpeed: 500,
            autoplay: false,
            nav: true,           
            dots: true,
            pauseOnHover: true,
            responsive:{
                600:{
                    margin: 20,
                    nav: true,
                    dots: true,
                    items: 2,
                },
                1000:{
                    margin: 20,
                    stagePadding: 10,
                    nav: true,
                    dots: true,
                    items: 3,
                }
            }
        });           
    }
})

$('.spoiler__title').click( function(e){
    if($('.block__spoiler').hasClass('one')){
        $('.spoiler__title').not($(this)).removeClass('active');
        $('.spoiler__content').not($(this).next()).slideUp(300);
    }
    $(this).toggleClass('active').next().slideToggle(300)
});

$(".panel__search__lupa").click( () => {
    if(window.innerWidth <= 700){
        $(".panel__search__lupa").toggleClass("active");
        $(".panel__search__input").toggleClass("active");
        return false;
    }
});
  
$(window).scroll( () => {
    if($(".panel__search__lupa").hasClass("active")){
        $(".panel__search__lupa").removeClass("active");
        $(".panel__search__input").removeClass("active");
    }
})
searchInput.onblur = () => {
    if($(".panel__search__input").hasClass("active")){
        $(".panel__search__input").removeClass("active");
    }
    if($(".panel__search__lupa").hasClass("active")){
        $(".panel__search__lupa").removeClass("active");
    }
}

const openNav = () => {
    if (window.innerWidth <= 420){
        $('#mySidenav').css({"width":"100%"})
    }
    else {        
        $('#mySidenav').css({"width":"75%"})
    }
    $('.dark-overlay').addClass('show');
}
const closeNav = () => {
    $('.dark-overlay').removeClass('show');
    $('#mySidenav').css({"width":"0%"})
    $('#mySidenav-subcaregories').css({"width":"0%"})
}

const openSubcaregoriesNav = () => {
    if (window.innerWidth <= 420){
        $('#mySidenav-subcaregories').css({"width":"100%"})
    }
    else {        
        $('#mySidenav-subcaregories').css({"width":"75%"})
    }
}
const closeSubcaregoriesNav = () =>{
    $('#mySidenav-subcaregories').css({"width":"0%"})
}

$(window).resize((e) => {
    if(window.innerWidth <= 700){
        hideSubcategories();
    }
})

window.addEventListener("scroll", () => { 
   var st = window.pageYOffset || document.documentElement.scrollTop; 
   var lastScrollTop = 0;
   if (st > lastScrollTop){
    if(document.documentElement.scrollTop > 0){
        $('.panel__user-links').slideUp(150);
        $('.panel__user-links').removeClass('show');
        hideSubcategories()
        $('.header').addClass('scroling');
        $('#mySidenav').addClass('scroling');
        $('.returnbtn').addClass('scroling');
      }
   } else {
      if(document.documentElement.scrollTop == 0){
        $('.header').removeClass('scroling');
        $('#mySidenav').removeClass('scroling');
        $('.returnbtn').removeClass('scroling')
      }
   }
   lastScrollTop = st <= 0 ? 0 : st;
}, false);

const showSubcategories = () => {   
    if(!$('.subcategories').hasClass('show')){
        $('.subcategories').slideDown(200);
        $('.subcategories').addClass('show')
        $('.subcategories').css('display', 'flex')
    }
}
const hideSubcategories = () => {
    if($('.subcategories').hasClass('show')){
        $('.subcategories').slideUp(200);
        $('.subcategories').removeClass('show')
    }
}
$(document).click((e) => {
    if(!e.target.closest('.subcategories, .category__link')){
        hideSubcategories();
    }
})
$('.subcategory').click((e) => {
    hideSubcategories();  
})

$('.panel__user').click( () => {
    if($('.panel__user-links').hasClass('show')){
        $('.panel__user-links').slideUp(150);
        $('.panel__user-links').removeClass('show');
    } else{
        $('.panel__user-links').slideDown(150);
        $('.panel__user-links').addClass('show');
    }
});

$('#password-control').click(function(e) {
	if ($('#password-input').attr('type') == 'password'){
		$(this).addClass('view');
		$('#password-input').attr('type', 'text');
	} else {
		$(this).removeClass('view');
		$('#password-input').attr('type', 'password');
	}
	return false;
})
$('#password-confirm-control').click( function(e) {
	if ($('#password-confirm-input').attr('type') == 'password'){
		$(this).addClass('view');
		$('#password-confirm-input').attr('type', 'text');
	} else {
		$(this).removeClass('view');
		$('#password-confirm-input').attr('type', 'password');
	}
	return false;
})

$(window).scroll( () => {
    if(document.documentElement.scrollTop > 700) {
        $('.scroll-to-up').addClass('active');
    } else{
        $('.scroll-to-up').removeClass('active');
    }
})
$('.scroll-to-up').click( () =>{
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    })
})

$('#email-input').on('input', e => {
    $('#email-label').addClass('shift')
    if(e.target.value == ""){
        $('#email-label').removeClass('shift')
    }
})
const scorePassword = (pass) => {
    var score = 0;
    if (!pass)
        return score;

    // award every unique letter until 5 repetitions
    var letters = new Object();
    for (var i=0; i<pass.length; i++) {
        letters[pass[i]] = (letters[pass[i]] || 0) + 1;
        score += 5.0 / letters[pass[i]];
    }

    // bonus points for mixing it up
    var variations = {
        digits: /\d/.test(pass),
        lower: /[a-z]/.test(pass),
        upper: /[A-Z]/.test(pass),
        nonWords: /\W/.test(pass),
    }

    var variationCount = 0;
    for (var check in variations) {
        variationCount += (variations[check] == true) ? 1 : 0;
    }
    score += (variationCount - 1) * 10;

    return parseInt(score);
}
$('#password-input').on('input', e => {
    $('#password-label').addClass('shift')

    var score = scorePassword(e.target.value)

    $('#password-meter').removeClass('strong high-medium medium simple')

    if(score >= 80){
        $('#password-meter').addClass('show strong')
        $('#password-meter-label').addClass('show')
        $('#password-meter-label').html('Сложный')
    } else if (score >= 60){
        $('#password-meter').addClass('show high-medium')
        $('#password-meter-label').addClass('show')
        $('#password-meter-label').html('Средний')
    } else if (score >= 40){
        $('#password-meter').addClass('show medium')
        $('#password-meter-label').addClass('show')
        $('#password-meter-label').html('Ниже среднего')
    } else if (score >= 20){
        $('#password-meter').addClass('show simple')
        $('#password-meter-label').addClass('show')
        $('#password-meter-label').html('Слабый')
    }

    if(e.target.value == ""){
        $('#password-meter').removeClass('show')
        $('#password-meter-label').removeClass('show')
        $('#password-label').removeClass('shift')
    }
})
$('#password-confirm-input').on('input', e => {
    $('#password-confirm-label').addClass('shift')
    if(e.target.value == ""){
        $('#password-confirm-label').removeClass('shift')
    }
})

$('.counter__plus').click(e => {
    const counterInput = $(e.target).prev()[0];
    counterInput.value++;
    if(counterInput.value > 1) $($(e.target).parent().next()[0]).slideDown(100);
    sum = (27999 * counterInput.value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    sumDiscount = (Math.round((27999 * counterInput.value) * 0.9)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    $($($($(e.target).parent()).parent()).parent().find('.block-price__price')  [0]).html(sum + ' ₽')
    $($($($(e.target).parent()).parent()).parent().find('.block-price__discount-price')[0]).html(sumDiscount + ' ₽')
})
$('.counter__minus').click(e => {
    const counterInput = $(e.target).next()[0]
    if(counterInput.value > 1) counterInput.value--;
    if(counterInput.value == 1) $($(e.target).parent().next()[0]).css('transform', 'translateY(-10px)').slideUp(100);
    sum = (27999 * counterInput.value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    sumDiscount = (Math.round((27999 * counterInput.value) * 0.9)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    $($($($(e.target).parent()).parent()).parent().find('.block-price__price')[0]).html(sum + ' ₽')
    $($($($(e.target).parent()).parent()).parent().find('.block-price__discount-price')[0]).html(sumDiscount + ' ₽')
})
$('.counter-input').on('input', e => {
    e.target.value = e.target.value.replace(/[^0-9]/g, '');
    if(e.target.value <= 1) {
        $($(e.target).parent().next()[0]).css('transform', 'translateY(-10px)').slideUp(100);
        e.target.value = 1;
    }
    if(e.target.value > 1) $($(e.target).parent().next()[0]).slideDown(100); 
    sum = (27999 * e.target.value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    sumDiscount = (Math.round((27999 * counterInput.value) * 0.9)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    $($($($(e.target).parent()).parent()).parent().find('.block-price__price')[0]).html(sum + ' ₽')
    $($($($(e.target).parent()).parent()).parent().find('.block-price__discount-price')[0]).html(sumDiscount + ' ₽')
})