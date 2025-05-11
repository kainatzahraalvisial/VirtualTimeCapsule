    //<script type="module">
    //    import * as THREE from 'three';

    //    // Three.js setup for Milky Way effect
    //    const scene = new THREE.Scene();
    //    const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    //    const renderer = new THREE.WebGLRenderer({ alpha: true });
    //    const container = document.getElementById('three-container');
    //    renderer.setSize(window.innerWidth, window.innerHeight);
    //    container.appendChild(renderer.domElement);

    //    // Milky Way particles (purple hue)
    //    const milkyWayGeometry = new THREE.BufferGeometry();
    //    const milkyWayCount = 10000;
    //    const mwPositions = new Float32Array(milkyWayCount * 3);
    //    const mwColors = new Float32Array(milkyWayCount * 3);
    //    for (let i = 0; i < milkyWayCount * 3; i += 3) {
    //        const radius = Math.random() * 500;
    //        const theta = Math.random() * Math.PI * 2;
    //        mwPositions[i] = Math.cos(theta) * radius;
    //        mwPositions[i + 1] = (Math.random() - 0.5) * 50;
    //        mwPositions[i + 2] = Math.sin(theta) * radius;
    //        mwColors[i] = 0.5 + Math.random() * 0.5; // Purple-red
    //        mwColors[i + 1] = 0; // Minimal green
    //        mwColors[i + 2] = 1; // Purple-blue
    //    }
    //    milkyWayGeometry.setAttribute('position', new THREE.BufferAttribute(mwPositions, 3));
    //    milkyWayGeometry.setAttribute('color', new THREE.BufferAttribute(mwColors, 3));
    //    const milkyWayMaterial = new THREE.PointsMaterial({ size: 2, vertexColors: true, transparent: true });
    //    const milkyWay = new THREE.Points(milkyWayGeometry, milkyWayMaterial);
    //    scene.add(milkyWay);

    //    // Particle man silhouette
    //    const manGeometry = new THREE.BufferGeometry();
    //    const manCount = 1000;
    //    const manPositions = new Float32Array(manCount * 3);
    //    for (let i = 0; i < manCount * 3; i += 3) {
    //        // Simple humanoid shape
    //        const x = (Math.random() - 0.5) * 20;
    //        const y = (Math.random() - 0.5) * 40;
    //        const z = (Math.random() - 0.5) * 10;
    //        manPositions[i] = x;
    //        manPositions[i + 1] = y;
    //        manPositions[i + 2] = z;
    //    }
    //    manGeometry.setAttribute('position', new THREE.BufferAttribute(manPositions, 3));
    //    const manMaterial = new THREE.PointsMaterial({ color: 0x00ffff, size: 3, transparent: true });
    //    const particleMan = new THREE.Points(manGeometry, manMaterial);
    //    particleMan.position.set(0, 0, -50);
    //    scene.add(particleMan);

    //    // Camera position
    //    camera.position.z = 100;

    //    // Mouse interaction
    //    let mouseX = 0, mouseY = 0;
    //    $(document).on('mousemove', (e) => {
    //        mouseX = (e.clientX / window.innerWidth) * 2 - 1;
    //        mouseY = -(e.clientY / window.innerHeight) * 2 + 1;
    //    });

    //    // Animation loop
    //    function animate() {
    //        requestAnimationFrame(animate);
    //        milkyWay.rotation.y += 0.001;
    //        camera.position.x += (mouseX * 50 - camera.position.x) * 0.05;
    //        camera.position.y += (mouseY * 50 - camera.position.y) * 0.05;
    //        camera.lookAt(scene.position);
    //        renderer.render(scene, camera);
    //    }
    //    animate();

    //    // Handle resize
    //    window.addEventListener('resize', () => {
    //        camera.aspect = window.innerWidth / window.innerHeight;
    //        camera.updateProjectionMatrix();
    //        renderer.setSize(window.innerWidth, window.innerHeight);
    //    });

    //    // Particle man animation on scroll
    //    ScrollTrigger.create({
    //        trigger: ".hero-section",
    //        start: "top top",
    //        end: "bottom top",
    //        scrub: true,
    //        onUpdate: (self) => {
    //            const progress = self.progress;
    //            const positions = particleMan.geometry.attributes.position.array;
    //            for (let i = 0; i < manCount * 3; i += 3) {
    //                const baseX = manPositions[i];
    //                const baseY = manPositions[i + 1];
    //                const baseZ = manPositions[i + 2];
    //                positions[i] = baseX + (Math.random() - 0.5) * progress * 100;
    //                positions[i + 1] = baseY + (Math.random() - 0.5) * progress * 100;
    //                positions[i + 2] = baseZ + (Math.random() - 0.5) * progress * 100;
    //            }
    //            particleMan.geometry.attributes.position.needsUpdate = true;
    //            particleMan.material.opacity = 1 - progress;
    //        }
    //    });

    //    // GSAP animations for hero section
    //    gsap.from(".hero-section h1", { opacity: 0, y: 50, duration: 1, delay: 0.5, ease: "power2.out" });
    //    gsap.from(".hero-section p", { opacity: 0, y: 50, duration: 1, delay: 0.7, ease: "2, ease: "power2.out" });
    //    gsap.from(".hero-section .cosmic-button", { opacity: 0, scale: 0.8, duration: 1, delay: 0.9, repeat: -1, yoyo: true, ease: "sine.inOut" });

    //    // GSAP animations for navbar
    //    gsap.from(".navbar-brand", { opacity: 0, x: -50, duration: 1, delay: 0.3, ease: "power2.out" });
    //    gsap.from(".navbar-nav .nav-link", {
    //        opacity: 0,
    //        x: 30,
    //        duration: 0.5,
    //        stagger: 0.1,
    //        delay: 0.5,
    //        ease: "power2.out"
    //    });

    //    // About section animations
    //    gsap.from(".about-item", {
    //        scrollTrigger: {
    //            trigger: ".about-section",
    //            start: "top 80%",
    //            end: "top 20%",
    //            scrub: true
    //        },
    //        opacity: 0,
    //        y: 50,
    //        duration: 1,
    //        stagger: 0.2
    //    });
    //</script>
    //<!-- // End of site.js -->

