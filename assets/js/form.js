// form.js
const form = document.querySelector(".contact-form");
form.addEventListener("submit", async (e) => {
  e.preventDefault();
  const action = form.getAttribute("action");
  const data = new FormData(form);
  try {
    const res = await fetch(action, {
      method: "POST",
      body: data,
      headers: { Accept: "application/json" },
    });

    if (res.ok) {
      Swal.fire({
        title: "¡Enviado!",
        text: "Mensaje enviado",
        icon: "success",
        confirmButtonColor: "#ff5722",
      });
      form.reset();
    } else {
      Swal.fire({
        title: "Error",
        text: "Intenta más tarde",
        icon: "error",
        confirmButtonColor: "#e64a19",
      });
    }
  } catch {
    Swal.fire({
      title: "Oops...",
      text: "Error de red",
      icon: "error",
      confirmButtonColor: "#e64a19",
    });
  }
});
