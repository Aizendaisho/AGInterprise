import { Card, CardHeader, CardTitle, CardContent } from "../components/ui/card";
import { motion } from "framer-motion";

interface FeatureCardProps {
  icon: React.ReactNode;
  title: string;
  description: string;
}

export function FeatureCard({ icon, title, description }: FeatureCardProps) {
  return (
    <motion.div
      className="flex justify-center"
      initial={{ scale: 0.8, opacity: 0 }}
      whileInView={{ scale: 1, opacity: 1 }}
      viewport={{ once: true }}
      transition={{ duration: 0.5 }}
    >
      <Card className="max-w-xs">
        <CardHeader className="flex flex-col items-center text-center space-y-2">
          <div className="text-4xl text-indigo-500">{icon}</div>
          <CardTitle>{title}</CardTitle>
        </CardHeader>
        <CardContent className="text-gray-600">
          {description}
        </CardContent>
      </Card>
    </motion.div>
  );
}
