function setActiveMenuItem() {
    const currentUrl = window.location.pathname; 
    const menuItems = document.querySelectorAll('.nav-link');

    menuItems.forEach(item => {
        const menuItemUrl = item.getAttribute('href'); 
        if (currentUrl === menuItemUrl) {
            item.classList.add('active'); 
        } else {
            item.classList.remove('active'); 
        }
    });
}


document.addEventListener("DOMContentLoaded", function () {
    setActiveMenuItem();
});