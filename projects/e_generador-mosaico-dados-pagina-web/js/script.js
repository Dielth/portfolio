// Referencias a elementos del DOM
const imageInput = document.getElementById('imageInput');
const detailLevel = document.getElementById('detailLevel');
const diceSize = document.getElementById('diceSize');
const maxWidth = document.getElementById('maxWidth');
const generateBtn = document.getElementById('generateBtn');
const downloadBtn = document.getElementById('downloadBtn');
const status = document.getElementById('status');
const canvas = document.getElementById('canvas');
const ctx = canvas.getContext('2d');
const progressContainer = document.getElementById('progress-container');
const progressBar = document.getElementById('progress-bar');
const loadingOverlay = document.querySelector('.loading-overlay');
const previewContainer = document.querySelector('.preview-container');
const previewImg = document.getElementById('preview-img');

// Variables globales
let originalImage = null;
let isProcessing = false;

// Imagen de los dados (6 valores)
const diceImg = new Image();
// Usar una imagen de dados SVG para mejor calidad
diceImg.src = 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIzMDAiIGhlaWdodD0iNTAiIHZpZXdCb3g9IjAgMCAzMDAgNTAiPgogIDwhLS0gRGllIDEgLS0+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjUwIiBoZWlnaHQ9IjUwIiByeD0iNSIgcnk9IjUiIGZpbGw9IndoaXRlIiBzdHJva2U9ImJsYWNrIiBzdHJva2Utd2lkdGg9IjEiLz4KICA8Y2lyY2xlIGN4PSIyNSIgY3k9IjI1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIAogIDwhLS0gRGllIDIgLS0+CiAgPHJlY3QgeD0iNTAiIHk9IjAiIHdpZHRoPSI1MCIgaGVpZ2h0PSI1MCIgcng9IjUiIHJ5PSI1IiBmaWxsPSJ3aGl0ZSIgc3Ryb2tlPSJibGFjayIgc3Ryb2tlLXdpZHRoPSIxIi8+CiAgPGNpcmNsZSBjeD0iNjUiIGN5PSIxNSIgcj0iNSIgZmlsbD0iYmxhY2siLz4KICA8Y2lyY2xlIGN4PSI4NSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIAogIDwhLS0gRGllIDMgLS0+CiAgPHJlY3QgeD0iMTAwIiB5PSIwIiB3aWR0aD0iNTAiIGhlaWdodD0iNTAiIHJ4PSI1IiByeT0iNSIgZmlsbD0id2hpdGUiIHN0cm9rZT0iYmxhY2siIHN0cm9rZS13aWR0aD0iMSIvPgogIDxjaXJjbGUgY3g9IjExNSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjEyNSIgY3k9IjI1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjEzNSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIAogIDwhLS0gRGllIDQgLS0+CiAgPHJlY3QgeD0iMTUwIiB5PSIwIiB3aWR0aD0iNTAiIGhlaWdodD0iNTAiIHJ4PSI1IiByeT0iNSIgZmlsbD0id2hpdGUiIHN0cm9rZT0iYmxhY2siIHN0cm9rZS13aWR0aD0iMSIvPgogIDxjaXJjbGUgY3g9IjE2NSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjE4NSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjE2NSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjE4NSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIAogIDwhLS0gRGllIDUgLS0+CiAgPHJlY3QgeD0iMjAwIiB5PSIwIiB3aWR0aD0iNTAiIGhlaWdodD0iNTAiIHJ4PSI1IiByeT0iNSIgZmlsbD0id2hpdGUiIHN0cm9rZT0iYmxhY2siIHN0cm9rZS13aWR0aD0iMSIvPgogIDxjaXJjbGUgY3g9IjIxNSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjIzNSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjIyNSIgY3k9IjI1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjIxNSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjIzNSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIAogIDwhLS0gRGllIDYgLS0+CiAgPHJlY3QgeD0iMjUwIiB5PSIwIiB3aWR0aD0iNTAiIGhlaWdodD0iNTAiIHJ4PSI1IiByeT0iNSIgZmlsbD0id2hpdGUiIHN0cm9rZT0iYmxhY2siIHN0cm9rZS13aWR0aD0iMSIvPgogIDxjaXJjbGUgY3g9IjI2NSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjI4NSIgY3k9IjE1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjI2NSIgY3k9IjI1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjI4NSIgY3k9IjI1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjI2NSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgogIDxjaXJjbGUgY3g9IjI4NSIgY3k9IjM1IiByPSI1IiBmaWxsPSJibGFjayIvPgo8L3N2Zz4=';

// Verificar que la imagen se haya cargado
diceImg.onload = function() {
    console.log("Imagen de dados cargada correctamente");
    status.textContent = "Listo para procesar imagen";
};

diceImg.onerror = function() {
    console.error("Error al cargar la imagen de dados");
    status.textContent = "Error al cargar recursos. Refresca la página.";
    status.style.color = "red";
};

// Escuchar cambios en el input de imagen
imageInput.addEventListener('change', function(e) {
    const file = e.target.files[0];
    if (!file) return;
    
    const reader = new FileReader();
    reader.onload = function(event) {
        originalImage = new Image();
        originalImage.onload = function() {
            // Mostrar vista previa de la imagen
            previewImg.src = event.target.result;
            previewContainer.style.display = 'flex';
            
            // Actualizar estado
            status.textContent = `Imagen cargada: ${file.name} (${originalImage.width}x${originalImage.height})`;
            status.style.color = "#4a90e2";
        };
        originalImage.src = event.target.result;
    };
    reader.readAsDataURL(file);
});

// Evento para generar el mosaico
generateBtn.addEventListener('click', generateMosaic);

// Evento para descargar el mosaico
downloadBtn.addEventListener('click', function() {
    if (!canvas.toDataURL) {
        status.textContent = "Tu navegador no soporta la descarga de imágenes";
        return;
    }
    
    const link = document.createElement('a');
    link.download = 'mosaico-dados.png';
    link.href = canvas.toDataURL('image/png');
    link.click();
});

// Función principal para generar el mosaico
function generateMosaic() {
    if (isProcessing) return;
    
    const file = imageInput.files[0];
    if (!file) {
        status.textContent = "Debes seleccionar una imagen primero";
        status.style.color = "red";
        return;
    }
    
    if (!originalImage || !originalImage.complete) {
        status.textContent = "La imagen aún no se ha cargado completamente";
        status.style.color = "red";
        return;
    }
    
    const detail = parseInt(detailLevel.value) || 3;
    const size = parseInt(diceSize.value) || 15;
    const maxCanvasWidth = parseInt(maxWidth.value) || 1000;
    
    // Marcar como procesando
    isProcessing = true;
    generateBtn.disabled = true;
    downloadBtn.disabled = true;
    status.textContent = "Procesando imagen...";
    status.style.color = "#aaa";
    progressContainer.style.display = 'block';
    progressBar.style.width = '0%';
    loadingOverlay.style.display = 'flex';
    
    // Usar setTimeout para permitir que la UI se actualice antes del procesamiento
    setTimeout(() => {
        // Calcular tamaño ajustado basado en el detalle y el tamaño máximo
        const scale = Math.max(1, Math.min(6, detail));
        let scaledWidth = Math.floor(originalImage.width / scale);
        let scaledHeight = Math.floor(originalImage.height / scale);
        
        // Limitar el tamaño para evitar problemas de rendimiento
        const aspectRatio = originalImage.width / originalImage.height;
        
        if (scaledWidth * size > maxCanvasWidth) {
            scaledWidth = Math.floor(maxCanvasWidth / size);
            scaledHeight = Math.floor(scaledWidth / aspectRatio);
        }
        
        // Asegurarse de que el tamaño sea razonable
        if (scaledWidth <= 0) scaledWidth = 1;
        if (scaledHeight <= 0) scaledHeight = 1;
        
        // Configurar canvas para la imagen procesada
        canvas.width = scaledWidth * size;
        canvas.height = scaledHeight * size;
        
        // Temporalmente dibujar la imagen original a tamaño reducido
        const tempCanvas = document.createElement('canvas');
        tempCanvas.width = scaledWidth;
        tempCanvas.height = scaledHeight;
        const tempCtx = tempCanvas.getContext('2d');
        tempCtx.drawImage(originalImage, 0, 0, scaledWidth, scaledHeight);
        
        // Obtener datos de píxeles
        const imageData = tempCtx.getImageData(0, 0, scaledWidth, scaledHeight);
        const pixels = imageData.data;
        
        // Limpiar canvas de resultado
        ctx.fillStyle = '#222';
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        
        // Dibujar dado por dado basado en el brillo
        const diceWidth = diceImg.width / 6; // Ancho de cada cara de dado
        const diceHeight = diceImg.height;   // Alto de cada cara de dado
        
        // Procesamiento por lotes para no bloquear la UI
        const batchSize = Math.ceil(scaledHeight / 20); // Procesar 20 filas a la vez
        let currentRow = 0;
        
        function processBatch() {
            const endRow = Math.min(currentRow + batchSize, scaledHeight);
            
            for (let y = currentRow; y < endRow; y++) {
                for (let x = 0; x < scaledWidth; x++) {
                    const i = (y * scaledWidth + x) * 4;
                    
                    // Calcular brillo y valor de dado solo si el pixel no es transparente
                    if (pixels[i + 3] > 127) { // Si no es transparente
                        const r = pixels[i];
                        const g = pixels[i + 1];
                        const b = pixels[i + 2];
                        
                        // Calcular brillo (0-255)
                        const brightness = (r * 0.299 + g * 0.587 + b * 0.114);
                        
                        // Mapear brillo a valores de dados (1-6)
                        // Mapeo invertido: 1 = claro, 6 = oscuro
                        const diceValue = Math.max(1, Math.min(6, Math.ceil((255 - brightness) / 42.5)));
                        
                        // Calcular ubicación en la imagen de dados
                        const srcX = (diceValue - 1) * diceWidth;
                        
                        // Dibujar la cara del dado
                        ctx.drawImage(
                            diceImg,
                            srcX, 0, diceWidth, diceHeight,
                            x * size, y * size, size, size
                        );
                    }
                }
            }
            
            // Actualizar progreso
            const progress = Math.min(100, Math.round((endRow / scaledHeight) * 100));
            progressBar.style.width = progress + '%';
            
            currentRow = endRow;
            
            if (currentRow < scaledHeight) {
                // Procesar el siguiente lote
                setTimeout(processBatch, 0);
            } else {
                // Finalizar el procesamiento
                finishProcessing();
            }
        }
        
        // Iniciar procesamiento por lotes
        processBatch();
        
        function finishProcessing() {
            // Marcar como completado
            isProcessing = false;
            generateBtn.disabled = false;
            downloadBtn.disabled = false;
            status.textContent = "¡Mosaico generado con éxito!";
            status.style.color = "#4a90e2";
            loadingOverlay.style.display = 'none';
            
            // Mantener la barra de progreso visible para mostrar que se completó
            progressBar.style.width = '100%';
            
            // Mostrar información sobre el mosaico generado
            const totalDice = scaledWidth * scaledHeight;
            status.textContent += ` (${scaledWidth}x${scaledHeight} dados, ${totalDice} dados en total)`;
        }
    }, 100);
}