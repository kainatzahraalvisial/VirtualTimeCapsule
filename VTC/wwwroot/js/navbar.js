//window.initNavbarEffect = function () {
//    console.log("initNavbarEffect has been initialized.");
//};

//document.addEventListener("DOMContentLoaded", function () {
//    const navbarUl = document.querySelector(".navbar");
//    const glowLine = document.querySelector(".glow-line");

//    document.querySelectorAll('.nav-item').forEach(item => {
//        item.addEventListener('mouseenter', (e) => {

//            const { left, width } = e.target.getBoundingClientRect();
//            const navbarLeft = navbar.getBoundingClientRect().left;

//            glowLine.style.width = `${width}px`;
//            glowLine.style.left = `${left - navbarLeft}px`;
//            console.log('Hovered:', e.target.textContent);

//        });

//    if (!glowLine || !navbarUl) {
//        console.error("Navbar or glow line not found!");
//        return;
//    }
//    console.log("Navbar and glow line found successfully!");


//    function moveGlow(target) {
//        const { left, width } = target.getBoundingClientRect();
//        const navbarLeft = navbarUl.getBoundingClientRect().left;
//        glowLine.style.transform = `translateX(${left - navbarLeft}px)`;
//        glowLine.style.width = `${width}px`;
//        glowLine.style.opacity = "1";
//        glowLine.style.boxShadow = "0px 0px 10px 3px cyan";
//    }

//    navLinks.forEach(link => {
//        link.addEventListener("mouseenter", () => moveGlow(link));
//    });

//    navbarUl.addEventListener("mouseleave", () => {
//        glowLine.style.width = "0";
//        glowLine.style.opacity = "0";
//        glowLine.style.boxShadow = "none";
//    });
//});
