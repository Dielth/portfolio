// main.js
const observer = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add("visible");
        observer.unobserve(entry.target);
      }
    });
  },
  { threshold: 0.2 }
);

document.querySelectorAll(".section").forEach((sec) => {
  observer.observe(sec);
});

document.addEventListener("DOMContentLoaded", () => {
  const card = document.querySelector(".profile-card");
  if (!card) return;
  card.addEventListener("click", () => {
    card.classList.toggle("expanded");
  });
});

function adjustLayout() {
  const isMobile = window.matchMedia("(max-width: 768px)").matches;
  if (isMobile && document.querySelector(".profile-card")) {
    const profileHeight = document.querySelector(".profile-card").offsetHeight;
    document.documentElement.style.setProperty(
      "--profile-height",
      `${profileHeight}px`
    );
  }
}

window.addEventListener("resize", adjustLayout);
window.addEventListener("orientationchange", adjustLayout);

adjustLayout();

let lastScroll = 0;
window.addEventListener("scroll", () => {
  const currentScroll = window.pageYOffset;
  const navbar = document.querySelector(".navbar");

  if (currentScroll > lastScroll) {
    navbar.classList.remove("visible");
  } else {
    navbar.classList.add("visible");
  }
  lastScroll = currentScroll;
});

document.querySelector(".hamburger").addEventListener("click", () => {
  document.querySelector(".nav-menu").classList.toggle("active");
});

function filterProjects(filter) {}

function initObservers() {
  const observerOptions = {
    threshold: 0.2,
  };

  const textObserver = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add("text-animate");
      }
    });
  }, observerOptions);

  const timelineObserver = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add("visible");
        }
      });
    },
    { threshold: 0.3 }
  );

  document.querySelectorAll(".timeline-item, .thought-card").forEach((el) => {
    timelineObserver.observe(el);
  });
}

initObservers();

const hamburger = document.querySelector(".hamburger");
const navMenu = document.querySelector(".nav-menu");

hamburger.addEventListener("click", () => {
  hamburger.classList.toggle("active");
  navMenu.classList.toggle("active");

  hamburger.style.transform = navMenu.classList.contains("active")
    ? "rotate(90deg)"
    : "rotate(0deg)";

  document.body.style.overflow = navMenu.classList.contains("active")
    ? "hidden"
    : "auto";
});

document.querySelectorAll(".nav-menu a").forEach((link) => {
  link.addEventListener("click", () => {
    hamburger.classList.remove("active");
    navMenu.classList.remove("active");
    document.body.style.overflow = "auto";
  });
});

window.addEventListener("scroll", () => {
  if (navMenu.classList.contains("active")) {
    hamburger.classList.remove("active");
    navMenu.classList.remove("active");
    document.body.style.overflow = "auto";
  }
});

const projectsData = [
  {
    id: 1,
    title: "Primer Proyecto",
    category: "academicos",
    year: "2024",
    services: "Diseño Web",
    description: "Mi primer proyecto académico Web. High Performance Lab.",
    mainImage: "assets/images/project-1.png",
    gallery: [
      "assets/images/project-1.png",
      "assets/images/gallery-image-1.png",
    ],
    url: "projects/a_high-performance-lab/index.html",
  },
  {
    id: 2,
    title: "Interfaz de Cafetería",
    category: "academicos",
    year: "2024",
    services: "Diseño Web, creacion de interfaces",
    description: "Desarrollo y creación de una interfaz de cafeteria.",
    mainImage: "assets/images/project-2.png",
    gallery: [
      "assets/images/project-2.png",
      "assets/images/gallery-image-2.png",
    ],
    url: "projects/b_Sistema-de-cafeteria-pagina-web/index.html",
  },
  {
    id: 3,
    title: "Interfaz de Supermercado",
    category: "academicos",
    year: "2024",
    services: "Diseño Web, creacion de interfaces",
    description:
      "Desarrollo y creación de una interfaz de supermercado online. Sin embargo, no es funcional. Solo es una interfaz.",
    mainImage: "assets/images/project-3.png",
    gallery: ["assets/images/project-3.png"],
    url: "projects/c_Super-UAQ-pagina-web/index.html",
  },
  {
    id: 4,
    title: "Proyecto de Cálculo realizado en Visual Basic",
    category: "academicos",
    year: "2025",
    services: "Diseño Web, Desarrollo en Visual Basic",
    description:
      "Desarrollo y creación de una pagina que almacena un proyecto de Cálculo. La misma página detalla como descargar la calculadora de cálculo integral y diferencial, realizada con Visual Basic.",
    mainImage: "assets/images/project-4.png",
    gallery: [
      "assets/images/project-4.png",
      "assets/images/gallery-image-3.png",
    ],
    url: "projects/d_aplicaciones-calculov2-pagina/index.html",
  },
  {
    id: 5,
    title: "Generador de Mosaico de Dados",
    category: "otros",
    year: "2025",
    services: "Diseño Web, Creación de programa en HTML y JavaScript",
    description:
      "Una pagina web que genera mosaicos de dados. Puedes elegir el nivel de detalle y el tamaño del dado. El mosaico se genera en tiempo real y puedes descargarlo como imagen.",
    mainImage: "assets/images/project-5.png",
    gallery: [
      "assets/images/project-5.png",
      "assets/images/gallery-image-4.png",
      "assets/images/gallery-image-5.png",
    ],
    url: "projects/e_generador-mosaico-dados-pagina-web/index.html",
  },
];

document.addEventListener("DOMContentLoaded", function () {
  const filterButtons = document.querySelectorAll(".filter-btn");
  const projectCards = document.querySelectorAll(".project-card");
  const projectModal = document.getElementById("projectModal");
  const closeModalBtn = document.querySelector(".close-modal");
  const modalTitle = document.getElementById("modalTitle");
  const modalYear = document.getElementById("modalYear");
  const modalServices = document.getElementById("modalServices");
  const modalDescription = document.getElementById("modalDescription");
  const modalMainImage = document.getElementById("modalMainImage");
  const modalGallery = document.getElementById("modalGallery");
  const prevProjectBtn = document.querySelector(".prev-project");
  const nextProjectBtn = document.querySelector(".next-project");

  let currentProjectId = null;

  filterButtons.forEach((button) => {
    button.addEventListener("click", function () {
      filterButtons.forEach((btn) => btn.classList.remove("active"));
      this.classList.add("active");

      const filterValue = this.getAttribute("data-filter");

      projectCards.forEach((card) => {
        card.style.animation = "none";
        card.offsetHeight;

        if (
          filterValue === "all" ||
          card.getAttribute("data-category") === filterValue
        ) {
          card.style.display = "block";
          card.style.animation = "fadeInUp 0.6s ease forwards";
        } else {
          card.style.display = "none";
        }
      });
    });
  });

  projectCards.forEach((card) => {
    card.addEventListener("click", function () {
      const projectId = parseInt(
        this.getAttribute("data-index") ||
          Array.from(projectCards).indexOf(this) + 1,
        10
      );
      openProjectModal(projectId);
    });
  });

  closeModalBtn.addEventListener("click", closeModal);

  window.addEventListener("click", function (event) {
    if (event.target === projectModal) {
      closeModal();
    }
  });

  document.addEventListener("keydown", function (event) {
    if (event.key === "Escape" && projectModal.classList.contains("active")) {
      closeModal();
    }
  });

  prevProjectBtn.addEventListener("click", function () {
    navigateProjects("prev");
  });

  nextProjectBtn.addEventListener("click", function () {
    navigateProjects("next");
  });

  function openProjectModal(projectId) {
    const project = projectsData[projectId - 1];
    if (!project) return;

    currentProjectId = projectId;

    modalTitle.textContent = project.title;
    modalYear.textContent = project.year;
    modalServices.textContent = project.services;
    modalDescription.textContent = project.description;
    modalMainImage.src = project.mainImage;
    modalMainImage.alt = project.title;

    modalGallery.innerHTML = "";
    project.gallery.forEach((image) => {
      const galleryItem = document.createElement("div");
      galleryItem.className = "gallery-item";

      const img = document.createElement("img");
      img.src = image;
      img.alt = project.title;

      galleryItem.appendChild(img);
      modalGallery.appendChild(galleryItem);

      galleryItem.addEventListener("click", function () {
        modalMainImage.src = image;
        document.querySelectorAll(".gallery-item").forEach((item) => {
          item.classList.remove("active");
        });
        this.classList.add("active");
      });
    });

    projectModal.classList.add("active");
    document.body.style.overflow = "hidden";

    setTimeout(() => {
      const modalContent = projectModal.querySelector(".modal-content");
      modalContent.style.opacity = "1";
      modalContent.style.transform = "translateY(0)";
    }, 50);
  }

  function closeModal() {
    const modalContent = projectModal.querySelector(".modal-content");
    modalContent.style.opacity = "0";
    modalContent.style.transform = "translateY(50px)";

    setTimeout(() => {
      projectModal.classList.remove("active");
      document.body.style.overflow = "";
    }, 300);
  }

  function navigateProjects(direction) {
    if (!currentProjectId) return;

    let nextId =
      direction === "next" ? currentProjectId + 1 : currentProjectId - 1;

    if (nextId > projectsData.length) nextId = 1;
    if (nextId < 1) nextId = projectsData.length;

    const modalContent = projectModal.querySelector(".modal-content");
    modalContent.style.opacity = "0";
    modalContent.style.transform =
      direction === "next" ? "translateX(-50px)" : "translateX(50px)";

    setTimeout(() => {
      openProjectModal(nextId);
      setTimeout(() => {
        modalContent.style.opacity = "1";
        modalContent.style.transform = "translateX(0)";
      }, 50);
    }, 300);
  }

  function initProjectIndices() {
    projectCards.forEach((card, index) => {
      card.setAttribute("data-index", index + 1);
    });
  }

  initProjectIndices();
});

document.querySelector(".projects-gallery").addEventListener("click", (e) => {
  const card = e.target.closest(".project-card[data-id]");
  if (!card) return;

  const id = parseInt(card.dataset.id, 10);
  const proyecto = projectsData.find((p) => p.id === id);
  if (!proyecto || !proyecto.url) return;

  window.open(proyecto.url, "_blank");
});
