//Global
var GLOBAL = {};
GLOBAL.HeaderDotNetReference = null;
GLOBAL.MainLayouteDotNetReference = null;
GLOBAL.PageDotNetReference = null;
GLOBAL.FooterDotNetReference = null;
function SetHeaderDotnetReference(headerDotNetReference) { GLOBAL.HeaderDotNetReference = headerDotNetReference; };
function SetPageDotnetReference(pageDotNetReference) { GLOBAL.PageDotNetReference = pageDotNetReference; };
function SetMainLayouteDotnetReference(mainLayoutDotNetReference) { GLOBAL.MainLayouteDotNetReference = mainLayoutDotNetReference; };
function SetFooterDotnetReference(footerDotNetReference) { GLOBAL.FooterDotNetReference = footerDotNetReference; };

//Document
function getCurrentWindowInnerWidth() { return window.innerWidth; }
function getCurrentScrollTop() { return document.documentElement.scrollTop; }
$(window).scroll((e) => {
    if ($('.subcategories').hasClass('show')) {
        GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSubcategoriesAsync');
    }
    if ($('.panel__search__lupa').hasClass("active") && $('.panel__search__input').hasClass("active")) {
        GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSearchPanel');
    }
    if (getCurrentScrollTop() >= 835) {
        GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('ShowScrollToUp');
    }
    else {
        GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('HideScrollToUp');
    }
})
$(document).click((e) => {
    if (!e.target.closest('.subcategories, .category__link')) {
        if ($('.subcategories').hasClass('show')) {
            GLOBAL.HeaderDotNetReference.invokeMethodAsync("HideSubcategoriesAsync");
        }
    }
    if (!e.target.closest('.panel__search__lupa, .panel__search__input')) {
        if ($(".panel__search__lupa").hasClass("active") && $(".panel__search__input").hasClass("active")) {
            GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSearchPanel');
        }
    }
})
$(window).resize((e) => {
    if ($('.subcategories').hasClass('show')) {
        GLOBAL.DotNetReference.invokeMethodAsync("HideSubcategoriesAsync");
    }
    if ($(".panel__search__lupa").hasClass("active") && $(".panel__search__input").hasClass("active")) {
        GLOBAL.DotNetReference.invokeMethodAsync('HideSearchPanel');
    }
})

//MainLayout
function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    })
}

//Header
function showSubcategories() {
    if (!$('.subcategories').hasClass('show')) {
        $('.subcategories').slideDown(200);
        $('.subcategories').css('display', 'flex');
    }
}
function hideSubcategories() {
    if ($('.subcategories').hasClass('show')) {
        $('.subcategories').slideUp(200);
    }
}
function showUserPanel() {
    if (!$('.panel__user-links').hasClass('show')) {
        $('.panel__user-links').slideDown(150);
    }
}
function hideUserPanel() {
    if ($('.panel__user-links').hasClass('show')) {
        $('.panel__user-links').slideUp(150);
    }
}

//Footer
function showSpoilerContent(spoilerContent) {
    $('.spoiler__content').not($(this)).slideUp(300);
    if ($(spoilerContent).hasClass('active')) {
        hideSpoilerContent(spoilerContent);
    } else {
        $(spoilerContent).slideDown(300);
    }
}
function hideSpoilerContent(spoilerContent) {
    console.log('Зачистка')
    $('.spoiler__content').not($(this)).slideUp(300);
    if (!$(spoilerContent).hasClass('active')) {
        $(spoilerContent).slideUp(300);
    } else {
        showSpoilerContent(spoilerContent);
    }
}





















const openNav = () => {
    if (window.innerWidth <= 420){
        $('#mySidenav').css({'width':'100%'})
    }
    else {        
        $('#mySidenav').css({'width':'75%'})
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
function passwordConfirmInputValueChange() {
    const passwordConfirmInput = document.getElementById('password-confirm-input')
    const passwordConfirmLabel = document.getElementById('password-confirm-label')

    if (passwordConfirmInput.value == "") {
        passwordConfirmLabel.classList.remove('shift')
    } else {
        passwordConfirmLabel.classList.add('shift')
    }
}

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