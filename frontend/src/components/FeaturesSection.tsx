import { FeatureCard } from "./FeatureCard";
import { ClipboardList, DollarSign, Bell } from "lucide-react";

export function FeaturesSection() {
  return (
    <section className="py-20 bg-white">
      <div className="max-w-6xl mx-auto px-8 grid gap-8 grid-cols-1 md:grid-cols-3">
        <FeatureCard
          icon={<ClipboardList />}
          title="Inventarios al Día"
          description="Controla tus existencias en tiempo real y evita quiebres de stock."
        />
        <FeatureCard
          icon={<DollarSign />}
          title="Facturación Sin Errores"
          description="Genera facturas completas y lleva un historial ordenado de cada venta."
        />
        <FeatureCard
          icon={<Bell />}
          title="Alertas Proactivas"
          description="Recibe notificaciones cuando el inventario esté por debajo del mínimo."
        />
      </div>
    </section>
  );
}
