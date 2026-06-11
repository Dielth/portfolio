// Clases
class Cliente {
    constructor(nombre, telefono, email, direccion, rfc) {
        this.nombre = nombre || "";
        this.telefono = telefono || "";
        this.email = email || "";
        this.direccion = direccion || "";
        this.rfc = rfc || "";
    }
}

class Producto {
    constructor(id, nombre, descripcion, precio, esPersonalizacion = false) {
        this.id = id || 0;
        this.nombre = nombre || "";
        this.descripcion = descripcion || "";
        this.precio = precio || 0;
        this.esPersonalizacion = esPersonalizacion;
    }
}

class Orden {
    constructor(id, rfc, productos, personalizaciones, fecha, precioTotal) {
        this.id = id || 0;
        this.rfc = rfc || "";
        this.productos = Array.isArray(productos) ? productos : [productos];
        this.personalizaciones = Array.isArray(personalizaciones) ? personalizaciones : [];
        this.fecha = fecha || "";
        this.precioTotal = precioTotal || 0;
    }
}

// Datos iniciales (ajustados a los .txt)
let clientes = [
    new Cliente("JuanPerezCamacho", "5551234", "juanperez@email.com", "Calle Letardo 123", "PECJ011215FLA"),
    new Cliente("AnaGomezCardenas", "5555678", "anagomez@email.com", "Calle Real 456", "GOCA981205YKE"),
    new Cliente("AlfredoMarquezCamacho", "5558756", "alfmrqu@email.com", "Calle Verde 710", "MACA970714RTA"),
    new Cliente("MarioGomezCardenas", "5558761", "mariogom@email.com", "Calle Real 479", "GOCM910504JIT")
];

let productos = [
    new Producto(1, "CafeEspresso", "DeOrigen", 25.00),
    new Producto(2, "CafeLatte", "Cremoso", 30.00),
    new Producto(3, "CafeCapuccino", "Espumoso", 35.00),
    new Producto(4, "LecheVegetal", "Almendra", 9.00, true),
    new Producto(5, "Extra", "Cafe", 9.00, true)
];

// Ajustar las órdenes para usar el nuevo formato de personalizaciones
let ordenes = [
    new Orden(1, "PECJ011215FLA", ["CafeEspresso"], [], "07/12", 25.00),
    new Orden(2, "GOCA981205YKE", ["CafeLatte"], ["LecheVegetal", "Extra"], "07/12", 48.00),
    new Orden(3, "GOCM910504JIT", ["CafeCapuccino"], ["LecheVegetal"], "07/12", 44.00)
];

// Inicializar datos en localStorage
function inicializarDatos() {
    if (!localStorage.getItem('clientes')) {
        localStorage.setItem('clientes', JSON.stringify(clientes));
    } else {
        clientes = JSON.parse(localStorage.getItem('clientes'));
    }
    
    if (!localStorage.getItem('productos')) {
        localStorage.setItem('productos', JSON.stringify(productos));
    } else {
        productos = JSON.parse(localStorage.getItem('productos'));
    }
    
    if (!localStorage.getItem('ordenes')) {
        localStorage.setItem('ordenes', JSON.stringify(ordenes));
    } else {
        ordenes = JSON.parse(localStorage.getItem('ordenes'));
    }
}

// Funciones de interfaz
function mostrarModal(tipo) {
    document.querySelectorAll('.modal').forEach(m => {
        m.style.display = 'none';
    });
    
    const modal = document.getElementById(`modal-${tipo}`);
    modal.style.display = 'flex';

    if (tipo === 'orden') {
        cargarSelectClientes();
        cargarSelectProductos();
        cargarSelectPersonalizaciones();
    }
}

function cargarSelectClientes() {
    const select = document.getElementById('rfc-orden');
    select.innerHTML = '<option value="">Seleccione un cliente</option>';
    clientes.forEach(cliente => {
        const option = document.createElement('option');
        option.value = cliente.rfc;
        option.textContent = `${cliente.nombre} (${cliente.rfc})`;
        select.appendChild(option);
    });
}

function cargarSelectProductos() {
    const selects = document.querySelectorAll('.producto-orden');
    selects.forEach(select => {
        select.innerHTML = '<option value="">Seleccione un producto</option>';
        productos.forEach(producto => {
            if (!producto.esPersonalizacion) {
                const option = document.createElement('option');
                option.value = producto.nombre;
                option.textContent = `${producto.nombre} ($${producto.precio.toFixed(2)})`;
                select.appendChild(option);
            }
        });
    });
}

function cargarSelectPersonalizaciones() {
    const container = document.getElementById('personalizaciones-container');
    container.innerHTML = ''; // Limpiar contenedor de personalizaciones

    // Añadir checkboxes para cada personalización disponible
    productos.forEach(producto => {
        if (producto.esPersonalizacion) {
            const checkbox = document.createElement('div');
            checkbox.className = 'personalizacion-item';
            checkbox.innerHTML = `
                <input type="checkbox" id="personalizacion-${producto.id}" class="personalizacion-check" value="${producto.nombre}">
                <label for="personalizacion-${producto.id}">${producto.nombre} (+$${producto.precio.toFixed(2)})</label>
            `;
            container.appendChild(checkbox);
        }
    });
}

function agregarProducto() {
    const container = document.getElementById('productos-container');
    const nuevoProducto = document.createElement('div');
    nuevoProducto.className = 'producto-item';
    nuevoProducto.innerHTML = `
        <select class="producto-orden" required>
            <option value="">Seleccione un producto</option>
        </select>
        <button type="button" class="eliminar-producto" onclick="eliminarProducto(this)">✖</button>
    `;
    container.appendChild(nuevoProducto);
    cargarSelectProductos();
}

function eliminarProducto(boton) {
    const item = boton.parentElement;
    if (document.querySelectorAll('.producto-item').length > 1) {
        item.remove();
    } else {
        alert("¡Debe haber al menos un producto!");
    }
}

function ocultarModales() {
    document.querySelectorAll('.modal').forEach(m => {
        m.style.display = 'none';
    });
}

// Mostrar listas
function mostrarLista(tipo) {
    const contenedor = document.getElementById('lista-contenedor');
    let html = `<h2>Lista de ${tipo}</h2><div class="tabla-contenedor"><table class="tabla-datos"><tr>`;
    
    switch(tipo) {
        case 'clientes':
            html += `<th>Nombre</th><th>Teléfono</th><th>Email</th><th>Dirección</th><th>RFC</th></tr>`;
            clientes.forEach(c => {
                html += `<tr>
                    <td>${c.nombre}</td>
                    <td>${c.telefono}</td>
                    <td>${c.email}</td>
                    <td>${c.direccion}</td>
                    <td>${c.rfc}</td>
                </tr>`;
            });
            break;
        case 'productos':
            html += `<th>ID</th><th>Nombre</th><th>Descripción</th><th>Precio</th><th>Tipo</th></tr>`;
            productos.forEach(p => {
                html += `<tr>
                    <td>${p.id}</td>
                    <td>${p.nombre}</td>
                    <td>${p.descripcion}</td>
                    <td>$${p.precio.toFixed(2)}</td>
                    <td>${p.esPersonalizacion ? "Personalización" : "Producto"}</td>
                </tr>`;
            });
            break;
        case 'ordenes':
            html += `
                <th>ID</th>
                <th>Cliente</th>
                <th>Productos</th>
                <th>Personalizaciones</th>
                <th>N° Productos</th>
                <th>Fecha</th>
                <th>Total</th>
            </tr>`;
            ordenes.forEach(o => {
                const cliente = clientes.find(c => c.rfc === o.rfc);
                html += `
                <tr>
                    <td>${o.id}</td>
                    <td>${cliente ? cliente.nombre : o.rfc}</td>
                    <td>${o.productos.join(', ')}</td>
                    <td>${o.personalizaciones && o.personalizaciones.length > 0 ? o.personalizaciones.join(', ') : "Sin personalizar"}</td>
                    <td>${o.productos.length}</td>
                    <td>${o.fecha}</td>
                    <td>$${o.precioTotal.toFixed(2)}</td>
                </tr>`;
            });
            break;
    }
    
    html += `</table></div>`;
    contenedor.innerHTML = html;
}

// Guardar datos
function guardarCliente() {
    const nombre = document.getElementById('nombre-cliente').value.trim();
    const rfc = document.getElementById('rfc-cliente').value.trim();
    if (!nombre || !rfc) {
        alert('Nombre y RFC son obligatorios');
        return;
    }
    
    const cliente = new Cliente(
        nombre,
        document.getElementById('telefono-cliente').value.trim(),
        document.getElementById('email-cliente').value.trim(),
        document.getElementById('direccion-cliente').value.trim(),
        rfc
    );
    
    const existente = clientes.findIndex(c => c.rfc === rfc);
    if (existente >= 0) clientes[existente] = cliente;
    else clientes.push(cliente);
    
    localStorage.setItem('clientes', JSON.stringify(clientes));
    ocultarModales();
    mostrarLista('clientes');
    mostrarNotificacion("Cliente guardado correctamente");
}

function guardarProducto() {
    const id = parseInt(document.getElementById('id-producto').value);
    const nombre = document.getElementById('nombre-producto').value.trim();
    const precio = parseFloat(document.getElementById('precio-producto').value);
    if (!id || !nombre || isNaN(precio)) {
        alert('ID, Nombre y Precio válidos son obligatorios');
        return;
    }
    
    const esPersonalizacion = document.getElementById('es-personalizacion').checked;
    
    const producto = new Producto(
        id,
        nombre,
        document.getElementById('descripcion-producto').value.trim(),
        precio,
        esPersonalizacion
    );
    
    const existente = productos.findIndex(p => p.id === id);
    if (existente >= 0) productos[existente] = producto;
    else productos.push(producto);
    
    localStorage.setItem('productos', JSON.stringify(productos));
    ocultarModales();
    mostrarLista('productos');
    mostrarNotificacion("Producto guardado correctamente");
}

function guardarOrden() {
    const id = parseInt(document.getElementById('id-orden').value);
    const rfc = document.getElementById('rfc-orden').value;
    const productosSeleccionados = Array.from(document.querySelectorAll('.producto-orden'))
                                      .map(select => select.value)
                                      .filter(val => val !== "");
    const fecha = document.getElementById('fecha-orden').value.trim();
    
    if (!id || !rfc || productosSeleccionados.length === 0 || !fecha) {
        alert('Complete todos los campos obligatorios');
        return;
    }
    
    // Recoger personalizaciones seleccionadas (checkboxes)
    const personalizacionesSeleccionadas = Array.from(document.querySelectorAll('.personalizacion-check:checked'))
                                               .map(check => check.value);
    
    // Calcular precio total: productos base
    let precioTotal = 0;
    productosSeleccionados.forEach(nombreProducto => {
        const producto = productos.find(p => p.nombre === nombreProducto);
        if (producto) precioTotal += producto.precio;
    });
    
    // Añadir precio de personalizaciones
    personalizacionesSeleccionadas.forEach(nombrePersonalizacion => {
        const personalizacion = productos.find(p => p.nombre === nombrePersonalizacion);
        if (personalizacion) precioTotal += personalizacion.precio;
    });
    
    const orden = new Orden(id, rfc, productosSeleccionados, personalizacionesSeleccionadas, fecha, precioTotal);
    const existente = ordenes.findIndex(o => o.id === id);
    if (existente >= 0) ordenes[existente] = orden;
    else ordenes.push(orden);
    
    localStorage.setItem('ordenes', JSON.stringify(ordenes));
    ocultarModales();
    mostrarLista('ordenes');
    mostrarNotificacion("Orden guardada correctamente");
}

// Mostrar notificación
function mostrarNotificacion(mensaje, esError = false) {
    const notificacion = document.getElementById('notification');
    const mensajeElem = document.getElementById('notification-message');
    
    mensajeElem.textContent = mensaje;
    notificacion.className = esError ? 'notification error show' : 'notification show';
    
    setTimeout(() => {
        notificacion.className = 'notification';
    }, 3000);
}

// Eventos
document.addEventListener('DOMContentLoaded', () => {
    inicializarDatos();
    mostrarLista('clientes');
    
    // Menú interactivo
    document.querySelectorAll('.boton-menu[data-target]').forEach(button => {
        button.addEventListener('click', function() {
            const targetId = this.getAttribute('data-target');
            const submenu = document.getElementById(targetId);
            submenu.classList.toggle('active');
        });
    });
    
    document.getElementById('toggle-menu').addEventListener('click', toggleMenu);
});

function toggleMenu() {
    const menu = document.getElementById('menu-lateral');
    menu.classList.toggle('expandido');
}