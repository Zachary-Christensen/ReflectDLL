import graphviz
import pandas as pd

g = graphviz.Digraph('G', filename='dependencies.gv')
g.node_attr['shape'] = 'box'

dependencies = pd.read_csv('Dependencies.csv')
dependencies.columns = ['Class', 'Dependency']

for index, row in dependencies.iterrows():
    g.edge(row['Class'], row['Dependency'])
g.view()


