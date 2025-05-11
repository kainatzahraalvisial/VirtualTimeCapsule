console.log("about-background.js loaded and running");

// Canvas Setup
const canvas = document.getElementById('background-canvas');
const ctx = canvas.getContext('2d');

if (!canvas || !ctx) {
    console.error("Canvas or context not found");
} else {
    // Set canvas size
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    console.log(`Canvas position: x=0, y=0, width=${canvas.width}, height=${canvas.height}`);

    // Nebula Gradient
    const gradient = ctx.createLinearGradient(0, 0, canvas.width, canvas.height);
    gradient.addColorStop(0, 'rgba(10, 10, 50, 1)'); // Dark blue
    gradient.addColorStop(0.5, 'rgba(50, 0, 80, 0.8)'); // Purple
    gradient.addColorStop(1, 'rgba(80, 20, 60, 0.6)'); // Pinkish

    // Stars
    const stars = [];
    const starCount = 200;
    for (let i = 0; i < starCount; i++) {
        stars.push({
            x: Math.random() * canvas.width,
            y: Math.random() * canvas.height,
            radius: Math.random() * 1.5 + 0.5,
            opacity: Math.random() * 0.5 + 0.3,
            twinkleSpeed: Math.random() * 0.05 + 0.02,
            baseOpacity: Math.random() * 0.5 + 0.3
        });
    }

    // Mouse Glow and Trail
    let mouse = { x: 0, y: 0 };
    const trail = [];
    const trailLength = 20;
    const glowRadius = 50;

    // Mouse Movement
    document.addEventListener('mousemove', (e) => {
        mouse.x = e.clientX;
        mouse.y = e.clientY;
        trail.push({ x: mouse.x, y: mouse.y, opacity: 1 });
        if (trail.length > trailLength) {
            trail.shift();
        }
    });

    // Scroll Offset for Parallax
    let scrollOffset = 0;
    window.addEventListener('scroll', () => {
        scrollOffset = window.scrollY;
    });

    // Animation Loop
    function animate() {
        requestAnimationFrame(animate);

        // Clear canvas
        ctx.fillStyle = gradient;
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        // Draw Stars with Parallax and Twinkle
        stars.forEach(star => {
            // Parallax effect
            const parallaxY = star.y + scrollOffset * 0.1;
            const wrappedY = ((parallaxY % canvas.height) + canvas.height) % canvas.height;

            // Twinkle effect
            star.opacity = star.baseOpacity + Math.sin(Date.now() * star.twinkleSpeed) * 0.2;

            // Brighten stars near mouse
            const dx = star.x - mouse.x;
            const dy = wrappedY - mouse.y;
            const distance = Math.sqrt(dx * dx + dy * dy);
            let glowEffect = 0;
            if (distance < glowRadius) {
                glowEffect = (glowRadius - distance) / glowRadius;
                star.opacity = Math.min(1, star.baseOpacity + glowEffect * 0.5);
            }

            // Draw star
            ctx.beginPath();
            ctx.arc(star.x, wrappedY, star.radius, 0, Math.PI * 2);
            ctx.fillStyle = `rgba(255, 255, 255, ${star.opacity})`;
            ctx.fill();
        });

        // Draw Mouse Glow and Trail
        trail.forEach((point, index) => {
            const opacity = point.opacity * (index / trailLength);
            const gradientGlow = ctx.createRadialGradient(point.x, point.y, 0, point.x, point.y, glowRadius);
            gradientGlow.addColorStop(0, `rgba(0, 255, 255, ${opacity * 0.8})`);
            gradientGlow.addColorStop(1, `rgba(0, 255, 255, 0)`);

            ctx.beginPath();
            ctx.arc(point.x, point.y, glowRadius * (index / trailLength), 0, Math.PI * 2);
            ctx.fillStyle = gradientGlow;
            ctx.fill();

            point.opacity -= 0.05;
        });

        console.log("Rendering frame");
    }
    animate();

    // Resize Handler
    window.addEventListener('resize', () => {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        console.log(`Canvas resized: width=${canvas.width}, height=${canvas.height}`);
    });

    // GSAP Animations
    console.log("Starting card animations");
    gsap.to("#card-writing", { opacity: 1, duration: 0.5, delay: 0.5, ease: "power2.inOut", onComplete: () => console.log("Card-writing animation completed") });
    gsap.from("#card-locking", {
        opacity: 0, y: 100, duration: 0.5, scrollTrigger: {
            trigger: "#card-locking",
            start: "top 80%",
            toggleActions: "play none none reverse",
            onEnter: () => console.log("Card-locking animation triggered")
        }, ease: "power2.inOut"
    });
    gsap.from("#card-unlocking", {
        opacity: 0, y: 100, duration: 0.5, scrollTrigger: {
            trigger: "#card-unlocking",
            start: "top 80%",
            toggleActions: "play none none reverse",
            onEnter: () => console.log("Card-unlocking animation triggered")
        }, ease: "power2.inOut"
    });

    console.log("Script execution completed");
}