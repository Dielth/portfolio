const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('visible');
        }
    });
}, { threshold: 0.1 });

document.querySelectorAll('.header, .section').forEach(el => observer.observe(el));


document.getElementById('downloadBtn').addEventListener('click', function(e) {
    let ripple = document.createElement('div');
    ripple.className = 'ripple';
    ripple.style.left = e.clientX - e.target.offsetLeft + 'px';
    ripple.style.top = e.clientY - e.target.offsetTop + 'px';
    this.appendChild(ripple);
    
    setTimeout(() => ripple.remove(), 1000);
});